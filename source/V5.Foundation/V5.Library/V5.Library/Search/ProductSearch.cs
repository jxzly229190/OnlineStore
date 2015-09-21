// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductSearch.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The product search.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library.Search
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;

    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;

    /// <summary>
    /// The product search.
    /// </summary>
    public class ProductSearch
    {
        /// <summary>
        /// The sync object.
        /// </summary>
        private static object syncObject = new object();

        /// <summary>
        /// The create index.
        /// </summary>
        /// <param name="barcode">
        /// The barcode.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="price">
        /// The price.
        /// </param>
        /// <param name="imageUrl">
        /// The image url.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="spell">
        /// The spell.
        /// </param>
        /// <param name="properties">
        /// The properties.
        /// </param>
        public void CreateIndex(string barcode, string name, string price, string imageUrl, string categoryID, string spell, string properties)
        {
            lock (syncObject)
            {
                var indexPath = ConfigurationManager.AppSettings["IndexDirectory"].Trim();

                var writer = new IndexWriter(indexPath, new StandardAnalyzer(), false);
                this.CreateDocument(writer, barcode, name, price, imageUrl, categoryID, spell, properties);
            }
        }

        /// <summary>
        /// The delete index.
        /// </summary>
        /// <param name="barcode">
        /// The barcode.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool DeleteIndex(string barcode)
        {
            if (!string.IsNullOrEmpty(barcode))
            {
                lock (syncObject)
                {
                    var indexPath = ConfigurationManager.AppSettings["IndexDirectory"].Trim();
                    IndexReader indexReader = null;

                    try
                    {
                        indexReader = IndexReader.Open(indexPath);
                        indexReader.DeleteDocuments(new Term("barcode", barcode));

                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        if (indexReader != null)
                        {
                            indexReader.Close();
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// The update index.
        /// </summary>
        /// <param name="barcode">
        /// The barcode.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="price">
        /// The price.
        /// </param>
        /// <param name="imageUrl">
        /// The image url.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="spell">
        /// The spell.
        /// </param>
        /// <param name="properties">
        /// The properties.
        /// </param>
        public void UpdateIndex(string barcode, string name, string price, string imageUrl, string categoryID, string spell, string properties)
        {
            if (this.DeleteIndex(barcode))
            {
                this.CreateIndex(barcode, name, price, imageUrl, categoryID, spell, properties);
            }
        }

        /// <summary>
        /// The get search result.
        /// </summary>
        /// <param name="queryString">
        /// The query string.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="pageCount">
        /// The page count.
        /// </param>
        /// <param name="totalCount">
        /// The total count.
        /// </param>
        /// <returns>
        /// 文档列表
        /// </returns>
        public List<Document> GetSearchResult(string queryString, string categoryID, int pageIndex, int pageSize, out int pageCount, out int totalCount)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                pageCount = 1;
                totalCount = 0;

                return null;
            }

            var indexPath = ConfigurationManager.AppSettings["IndexDirectory"].Trim();
            IndexSearcher indexSearcher = null;

            try
            {
                indexSearcher = new IndexSearcher(indexPath);
                var startIndex = (pageIndex - 1) * pageSize;
                var endIndex = pageIndex * pageSize;

                Analyzer analyzer = new StandardAnalyzer();

                var fields = new[] { "barcode", "name", "price", "imageUrl", "price" };
                QueryParser parser = new MultiFieldQueryParser(fields, analyzer);

                var query = parser.Parse(queryString);

                var booleanQuery = new BooleanQuery();
                booleanQuery.Add(query, BooleanClause.Occur.MUST);
                if (string.IsNullOrEmpty(categoryID) && categoryID != "0")
                {
                    booleanQuery.Add(new QueryParser("categoryID", analyzer).Parse(categoryID), BooleanClause.Occur.MUST);
                }

                var hits = indexSearcher.Search(booleanQuery);
                if (hits.Length() == 0)
                {
                    pageCount = 1;
                    totalCount = 0;

                    return null;
                }

                totalCount = hits.Length();
                endIndex = Math.Min(endIndex, totalCount);
                if (totalCount % pageSize != 0)
                {
                    pageCount = (totalCount / pageSize) + 1;
                }
                else
                {
                    pageCount = totalCount / pageSize;
                }

                var documents = new List<Document>();

                for (var i = startIndex; i < endIndex; i++)
                {
                    documents.Add(hits.Doc(i));
                }

                return documents;
            }
            catch (IOException)
            {
                pageCount = 1;
                totalCount = 0;

                return null;
            }
            finally
            {
                if (indexSearcher != null)
                {
                    indexSearcher.Close();
                }
            }
        }

        /// <summary>
        /// The create document.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <param name="barcode">
        /// The barcode.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="price">
        /// The price.
        /// </param>
        /// <param name="imageUrl">
        /// The image url.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="spell">
        /// The spell.
        /// </param>
        /// <param name="properties">
        /// The properties.
        /// </param>
        private void CreateDocument(IndexWriter writer, string barcode, string name, string price, string imageUrl, string categoryID, string spell, string properties)
        {
            var doc = new Document();

            doc.Add(new Field("barcode", barcode, Field.Store.YES, Field.Index.TOKENIZED));
            doc.Add(new Field("name", name, Field.Store.YES, Field.Index.TOKENIZED));
            doc.Add(new Field("price", price, Field.Store.YES, Field.Index.TOKENIZED));
            doc.Add(new Field("imageUrl", imageUrl, Field.Store.YES, Field.Index.UN_TOKENIZED));
            doc.Add(new Field("categoryID", categoryID, Field.Store.YES, Field.Index.TOKENIZED));
            doc.Add(new Field("spell", spell, Field.Store.YES, Field.Index.TOKENIZED));
            doc.Add(new Field("properties", properties, Field.Store.YES, Field.Index.TOKENIZED));

            writer.AddDocument(doc);
        }
    }
}