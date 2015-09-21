using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using V5.Portal.Models;
using V5.Library;
using System.Threading;

namespace V5.Portal.Filters
{
    /// <summary>
    /// 页面静态化
    /// </summary>
    public class StaticFileWriteFilterAttribute : FilterAttribute, IResultFilter
    {
        private static Mutex mut = new Mutex();

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            mut.WaitOne();

            int i = 0;
            try
            {
                while (i < 2)
                {
                    i++;
                    bool success = DelFile(filterContext);
                    if (success) break;
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                V5.Library.Logger.LogUtils.Log(ex.Message, "StaticFileWriteResponseFilterWrapper.OnResultExecuted", Library.Logger.Category.Fatal);
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Filter = new StaticFileWriteResponseFilterWrapper(filterContext.HttpContext.Response.Filter, filterContext);
        }

        public bool DelFile(ResultExecutedContext filterContext)
        {
            try
            {
                string path = filterContext.HttpContext.Request.Path;
                string file = path == "/Home/Index" || path == "/" ? filterContext.HttpContext.Server.MapPath("~/index.htm") : filterContext.HttpContext.Server.MapPath(path);
                if (Path.HasExtension(file))
                {
                    string dir = Path.GetDirectoryName(file);
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                V5.Library.Logger.LogUtils.Log(ex.Message, "StaticFileWriteResponseFilterWrapper.DelFile", Library.Logger.Category.Fatal);
                return false;
            }
        }

        class StaticFileWriteResponseFilterWrapper : System.IO.Stream
        {
            private System.IO.Stream inner;
            private ControllerContext context;
            public StaticFileWriteResponseFilterWrapper(System.IO.Stream s, ControllerContext context)
            {
                this.inner = s;
                this.context = context;
            }

            public override bool CanRead
            {
                get { return inner.CanRead; }
            }

            public override bool CanSeek
            {
                get { return inner.CanSeek; }
            }

            public override bool CanWrite
            {
                get { return inner.CanWrite; }
            }

            public override void Flush()
            {
                inner.Flush();
            }

            public override long Length
            {
                get { return inner.Length; }
            }

            public override long Position
            {
                get
                {
                    return inner.Position;
                }
                set
                {
                    inner.Position = value;
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return inner.Read(buffer, offset, count);
            }

            public override long Seek(long offset, System.IO.SeekOrigin origin)
            {
                return inner.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                inner.SetLength(value);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                inner.Write(buffer, offset, count);
                try
                {
                    string path = HttpContext.Current.Request.Path;
                    string file = path == "/Home/Index" || path == "/" ? context.HttpContext.Server.MapPath("~/index.htm") : context.HttpContext.Server.MapPath(path);
                    if (Path.HasExtension(file))
                    {
                        string dir = Path.GetDirectoryName(file);
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        File.AppendAllText(file, System.Text.Encoding.UTF8.GetString(buffer));
                    }
                }
                catch(Exception ex)
                {
                    V5.Library.Logger.LogUtils.Log(ex.Message, "StaticFileWriteResponseFilterWrapper.Write", Library.Logger.Category.Fatal);
                }
            }
        }
    }
}