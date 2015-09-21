namespace V5.Library.Storage
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public class FileStore
    {
        #region Public Methods and Operators

        #region File

        public bool FileExists(string fileFullName)
        {
            if (string.IsNullOrEmpty(fileFullName))
            {
                throw new ArgumentNullException("fileFullName");
            }

            try
            {
                return System.IO.File.Exists(fileFullName);
            }
            catch (System.IO.IOException ioException)
            {
                throw new System.IO.IOException(ioException.Message, ioException);
            }
        }

        public bool FileExists(string filePath, string fileName)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            return this.FileExists(this.Combine(filePath, fileName));
        }

        public void CreateFile(string fileFullName, string fileContent, Encoding encoding)
        {
            if (string.IsNullOrEmpty(fileFullName))
            {
                throw new ArgumentNullException("fileFullName");
            }

            if (string.IsNullOrEmpty(fileContent))
            {
                throw new ArgumentNullException("fileContent");
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            try
            {
                using (var streamWriter = new System.IO.StreamWriter(fileFullName, false, encoding))
                {
                    streamWriter.Write(fileContent);
                }
            }
            catch (System.IO.IOException ioException)
            {
                throw new System.IO.IOException(ioException.Message, ioException);
            }
        }

        public void CreateFile(string directoryPath, string fileName, string fileContent, Encoding encoding)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentNullException("directoryPath");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            this.CreateFile(this.Combine(directoryPath, fileName), fileContent, encoding);
        }

        public void DeleteFile(string fileFullName)
        {
            if (string.IsNullOrEmpty(fileFullName))
            {
                throw new ArgumentNullException("fileFullName");
            }

            try
            {
                if (System.IO.File.Exists(fileFullName))
                {
                    System.IO.File.Delete(fileFullName);
                }
            }
            catch (System.IO.IOException ioException)
            {
                throw new System.IO.IOException(ioException.Message, ioException);
            }
        }

        public void DeleteFile(string filePath, string fileName)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            this.DeleteFile(this.Combine(filePath, fileName));
        }

        public System.IO.Stream OpenFile(string fileFullName)
        {
            if (string.IsNullOrEmpty(fileFullName))
            {
                throw new ArgumentNullException("fileFullName");
            }

            try
            {
                if (System.IO.File.Exists(fileFullName))
                {
                    return System.IO.File.OpenRead(fileFullName);
                }
            }
            catch (System.IO.IOException ioException)
            {
                throw new System.IO.IOException(ioException.Message, ioException);
            }

            return null;
        }

        public System.IO.Stream OpenFile(string filePath, string fileName)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            return this.OpenFile(this.Combine(filePath, fileName));
        }

        public void CopyFile(string sourceFileName, string targetFileName, bool overwrite)
        {
            if (string.IsNullOrEmpty(sourceFileName))
            {
                throw new ArgumentNullException("sourceFileName");
            }

            if (string.IsNullOrEmpty(targetFileName))
            {
                throw new ArgumentNullException("targetFileName");
            }

            try
            {
                if (this.FileExists(sourceFileName))
                {
                    System.IO.File.Copy(sourceFileName, targetFileName, overwrite);
                }
            }
            catch (System.IO.IOException ioException)
            {
                throw new System.IO.IOException(ioException.Message, ioException);
            }
        }

        public void CopyFile(string sourcePath, string targetPath, string fileName, bool overwrite)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentNullException("sourcePath");
            }

            if (string.IsNullOrEmpty(targetPath))
            {
                throw new ArgumentNullException("targetPath");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            var sourceFileName = this.Combine(sourcePath, fileName);
            var targetFileName = this.Combine(targetPath, fileName);

            this.CopyFile(sourceFileName, targetFileName, overwrite);
        }

        public void MoveFile(string sourceFileName, string targetFileName)
        {
            if (string.IsNullOrEmpty(sourceFileName))
            {
                throw new ArgumentNullException("sourceFileName");
            }

            if (string.IsNullOrEmpty(targetFileName))
            {
                throw new ArgumentNullException("targetFileName");
            }

            try
            {
                if (this.FileExists(sourceFileName))
                {
                    System.IO.File.Move(sourceFileName, targetFileName);
                }
            }
            catch (System.IO.IOException ioException)
            {
                throw new System.IO.IOException(ioException.Message, ioException);
            }
        }

        public void MoveFile(string sourcePath, string targetPath, string fileName)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentNullException("sourcePath");
            }

            if (string.IsNullOrEmpty(targetPath))
            {
                throw new ArgumentNullException("targetPath");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            var sourceFileName = this.Combine(sourcePath, fileName);
            var targetFileName = this.Combine(targetPath, fileName);

            this.MoveFile(sourceFileName, targetFileName);
        }

        public DateTimeOffset? GetCreated(string filePath, string fileName)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("fileName");
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("fileName");
            }

            var fileFullName = this.Combine(filePath, fileName);

            if (this.FileExists(fileFullName))
            {
                return System.IO.File.GetCreationTimeUtc(fileFullName);
            }

            return null;
        }

        public DateTimeOffset? GetModified(string filePath, string fileName)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("fileName");
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("fileName");
            }

            var fileFullName = this.Combine(filePath, fileName);

            if (this.FileExists(fileFullName))
            {
                return new System.IO.FileInfo(fileFullName).LastWriteTimeUtc;
            }

            return null;
        }

        public long GetSize(string filePath, string fileName)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            var fileFullName = this.Combine(filePath, fileName);

            if (this.FileExists(fileFullName))
            {
                return new System.IO.FileInfo(fileFullName).Length;
            }

            return 0;
        }

        #endregion

        #region Directory

        public bool DirectoryExists(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentNullException("directoryPath");
            }

            return System.IO.Directory.Exists(directoryPath);
        }

        public void CreateDirectory(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentNullException("directoryPath");
            }

            try
            {
                if (!System.IO.Directory.Exists(directoryPath))
                {
                    System.IO.Directory.CreateDirectory(directoryPath);
                }
            }
            catch (IOException ioException)
            {
                throw new IOException(ioException.Message, ioException);
            }
        }

        public void DeleteDirectory(string directoryPath, bool recursive)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentNullException("directoryPath");
            }

            System.IO.Directory.Delete(directoryPath, recursive);
        }

        public void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            if (string.IsNullOrEmpty(sourceDirectory))
            {
                throw new ArgumentNullException("sourceDirectory");
            }

            if (string.IsNullOrEmpty(targetDirectory))
            {
                throw new ArgumentNullException("targetDirectory");
            }

            try
            {
                this.CreateDirectory(targetDirectory);

                var fileNames = System.IO.Directory.GetFileSystemEntries(sourceDirectory);

                foreach (var fileName in fileNames)
                {
                    if (System.IO.Directory.Exists(fileName))
                    {
                        this.CopyDirectory(fileName + "\\", targetDirectory + Path.GetFileName(fileName) + "\\");
                    }
                    else
                    {
                        this.CopyFile(fileName, targetDirectory + Path.GetFileName(fileName), true);
                    }
                }
            }
            catch (IOException ioException)
            {
                throw new IOException(ioException.Message, ioException);
            }
        }

        public void CopyDirectory(string sourcePath, string targetPath, string directoryName)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentNullException("sourcePath");
            }

            if (string.IsNullOrEmpty(targetPath))
            {
                throw new ArgumentNullException("targetPath");
            }

            if (string.IsNullOrEmpty(directoryName))
            {
                throw new ArgumentNullException("directoryName");
            }

            var sourceDirectory = this.Combine(sourcePath, directoryName);
            var targetDirectory = this.Combine(sourcePath, directoryName);

            this.CopyDirectory(sourceDirectory, targetDirectory);
        }

        public void MoveDirectory(string sourceDirectory, string targetDirectory)
        {
            if (string.IsNullOrEmpty(sourceDirectory))
            {
                throw new ArgumentNullException("sourceDirectory");
            }

            if (string.IsNullOrEmpty(targetDirectory))
            {
                throw new ArgumentNullException("targetDirectory");
            }

            try
            {
                System.IO.Directory.Move(sourceDirectory, targetDirectory);
            }
            catch (IOException ioException)
            {
                throw new IOException(ioException.Message, ioException);
            }
        }

        public void MoveDirectory(string sourcePath, string targetPath, string directoryName)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentNullException("sourcePath");
            }

            if (string.IsNullOrEmpty(targetPath))
            {
                throw new ArgumentNullException("targetPath");
            }

            if (string.IsNullOrEmpty(directoryName))
            {
                throw new ArgumentNullException("directoryName");
            }

            var sourceDirectory = this.Combine(sourcePath, directoryName);
            var targetDirectory = this.Combine(sourcePath, directoryName);

            this.MoveDirectory(sourceDirectory, targetDirectory);
        }

        public IEnumerable<string> GetFileNames(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentNullException("directoryPath");
            }

            return this.GetFileNames(directoryPath, "*.*");
        }

        public IEnumerable<string> GetFileNames(string directoryPath, string filter)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentNullException("directoryPath");
            }

            if (string.IsNullOrEmpty(filter))
            {
                throw new ArgumentNullException("filter");
            }

            try
            {
                if (!directoryPath.EndsWith(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal))
                {
                    directoryPath = directoryPath + Path.DirectorySeparatorChar;
                }

                if (System.IO.Directory.Exists(directoryPath))
                {
                    return System.IO.Directory.EnumerateFiles(directoryPath, filter);
                }
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("没有获取指定目录的权限", unauthorizedAccessException);
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                throw new UnauthorizedAccessException("找不到指定目录", directoryNotFoundException);
            }

            return Enumerable.Empty<string>();
        }

        public DateTimeOffset? GetCreated(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentNullException("directoryPath");
            }

            if (this.DirectoryExists(directoryPath))
            {
                return System.IO.Directory.GetCreationTimeUtc(directoryPath);
            }

            return null;
        }

        public DateTimeOffset? GetModified(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentNullException("directoryPath");
            }

            if (this.DirectoryExists(directoryPath))
            {
                return new DirectoryInfo(directoryPath).LastWriteTimeUtc;
            }

            return null;
        }

        #endregion

        #endregion

        #region Methods

        public string Combine(string filePath, string fileName)
        {
            string fileFullName;

            if (filePath.EndsWith("\\"))
            {
                fileFullName = filePath + fileName;
            }
            else
            {
                fileFullName = filePath + "\\" + fileName;
            }

            return fileFullName;
        }

        #endregion
    }
}