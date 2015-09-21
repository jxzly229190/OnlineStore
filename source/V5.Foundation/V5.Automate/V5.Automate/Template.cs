namespace V5.Automate
{
    using System;
    using System.Configuration;
    using System.Collections.Generic;
    using System.Text;

    public class Template
    {
        #region Constants and Fields

        private int year;

        public string nameSpace;

        public string className;

        public List<string> usingList;

        private string corporation;

        public const string Header =
            @"// --------------------------------------------------------------------------------------------------------------------"+ "\r\n" +
            "// <copyright file=" + "\"{0}.cs\"" + " company=" + "\"{1}\"" + ">\r\n" +
            "//   (C) {2} {3}. All rights reserved." + "\r\n" +
            "// </copyright>\r\n" +
            "// <summary>\r\n" +
            "//   {4}\r\n" +
            "// </summary>\r\n" +
            "// --------------------------------------------------------------------------------------------------------------------\r\n";

        public const string ClassSummary = "\r\n    /// <summary>\r\n" +
                                           "    /// {0}\r\n" +
                                           "    /// </summary>\r\n";

        public const string PropertySummary = "\r\n        /// <summary>\r\n" +
                                              "        /// {0}\r\n" +
                                              "        /// </summary>\r\n";

        public const string ConstantsAndFields = "        #region Constants and Fields\r\n\r\n{0}\r\n\r\n        #endregion";

        public const string ConstructorsAndDestructors = "        #region Constructors and Destructors\r\n\r\n{0}\r\n\r\n        #endregion";

        public const string PublicProperties = "        #region Public Properties\r\n{0}\r\n        #endregion";

        public const string PublicIndexers = "        #region Public Indexers\r\n\r\n{0}\r\n\r\n        #endregion";

        public const string PublicMethodsAndOperators = "        #region Public Methods and Operators\r\n\r\n{0}\r\n\r\n        #endregion";

        public const string Methods = "        #region Methods\r\n\r\n{0}\r\n\r\n        #endregion";

        #endregion

        #region Constructors and Destructors

        public Template(string nameSpace, string className)
        {
            if (string.IsNullOrEmpty(nameSpace))
            {
                throw new ArgumentNullException("nameSpace");
            }

            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentNullException("className");
            }
            
            this.nameSpace = nameSpace;
            this.className = className;
        }

        public Template(string nameSpace, string className, List<string> usingList)
        {
            if (string.IsNullOrEmpty(nameSpace))
            {
                throw new ArgumentNullException("nameSpace");
            }

            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentNullException("className");
            }

            if (usingList == null)
            {
                throw new ArgumentNullException("usingList");
            }

            this.nameSpace = nameSpace;
            this.usingList = usingList;
            this.className = className;
        }

        #endregion

        #region Public Properties

        public int Year
        {
            get
            {
                if (this.year <= 0)
                {
                    this.year = DateTime.Now.Year;
                }

                return this.year;
            }
        }

        public string NameSpace
        {
            get
            {
                return this.nameSpace;
            }
        }

        public string ClassName
        {
            get
            {
                return this.className;
            }
        }

        public string Corporation
        {
            get
            {
                if (string.IsNullOrEmpty(this.corporation))
                {
                    this.corporation = ConfigurationManager.AppSettings["Corporation"];
                }

                return this.corporation;
            }
        }

        public string UsingList
        {
            get
            {
                if (this.usingList == null)
                {
                    return string.Empty;
                }

                return string.Join(@"\r\n    ", this.usingList);
            }
        }

        #endregion

        #region Public Methods and Operators

        public string GetEntityContent(DbTable dbTable)
        {
            if (dbTable == null)
            {
                throw new ArgumentNullException("dbTable");
            }

            var stringBuilder = new StringBuilder();

            var headerString = this.FileHeader(dbTable.Summary);

            var nameSpaceString = "\r\n" + "namespace " + this.NameSpace + "\r\n" + "{";

            var usingListString = string.Empty;
            if (!string.IsNullOrEmpty(this.UsingList))
            {
                usingListString = "\r\n    " + this.UsingList + "\r\n";
            }

            var classSummaryString = this.GetClassSummary(dbTable.Summary);
            var classCodeString = this.GetClassCode() + "\r\n    {\r\n";

            var properties = new StringBuilder();
            foreach (var dbColumn in dbTable.DbColumns)
            {
                var propertySummary = this.GetPropertySummary(dbColumn.Summary);
                var propertyCode = "        " + this.GetPropertyCode(dbColumn);

                properties.Append(propertySummary);
                properties.Append(propertyCode);
                properties.Append("\r\n");
            }

            var propertyString = string.Format(PublicProperties, properties);

            stringBuilder.Append(headerString);
            stringBuilder.Append(nameSpaceString);
            stringBuilder.Append(usingListString);
            stringBuilder.Append(classSummaryString);
            stringBuilder.Append(classCodeString);
            stringBuilder.Append(propertyString);
            stringBuilder.Append("\r\n    }\r\n}");

            return stringBuilder.ToString();
        }

        #endregion

        #region Methods

        private string FileHeader(string summary)
        {
            return string.Format(Header, this.ClassName, this.Corporation, this.Year, this.Corporation, summary);
        }

        private string GetClassSummary(string summary)
        {
            return string.Format(ClassSummary, summary);
        }

        private string GetClassCode()
        {
            return "    public class " + this.ClassName;
        }

        private string GetPropertySummary(string summary)
        {
            return string.Format(PropertySummary, "获取或设置" + summary + "．");
        }

        private string GetPropertyCode(DbColumn dbColumn)
        {
            if (dbColumn == null)
            {
                throw new ArgumentNullException("dbColumn");
            }

            return "public " + dbColumn.Type + " " + dbColumn.Name + " { get; set; }";
        }

        #endregion
    }
}