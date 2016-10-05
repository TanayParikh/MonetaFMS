// ZipStorer, by Jaime Olivares
// Website: zipstorer.codeplex.com
// Version: 2.35 (March 14, 2010)

using System.Collections.Generic;
using System.Text;

namespace System.IO.Compression
{
    /// <summary>
    /// Unique class for compression/decompression file. Represents a Zip file.
    /// </summary>
    public class ZipStorer : IDisposable
    {
        /// <summary>
        /// Compression method enumeration
        /// </summary>
        public enum Compression : ushort { 
            /// <summary>Uncompressed storage</summary> 
            Store = 0, 
            /// <summary>Deflate compression method</summary>
            Deflate = 8 }

        /// <summary>
        /// Represents an entry in Zip file directory
        /// </summary>
        public struct ZipFileEntry
        {
            /// <summary>Compression method</summary>
            public Compression method; 
            /// <summary>Full path and filename as stored in Zip</summary>
            public string filenameInZip;
            /// <summary>Original file size</summary>
            public uint fileSize;
            /// <summary>Compressed file size</summary>
            public uint compressedSize;
            /// <summary>Offset of header information inside Zip storage</summary>
            public uint headerOffset;
            /// <summary>Offset of file inside Zip storage</summary>
            public uint fileOffset;
            /// <summary>Size of header information</summary>
            public uint headerSize;
            /// <summary>32-bit checksum of entire file</summary>
            public uint crc32;
            /// <summary>Last modification time of file</summary>
            public DateTime modifyTime;
            /// <summary>User comment for file</summary>
            public string comment;
            /// <summary>True if UTF8 encoding for filename and comments, false if default (CP 437)</summary>
            public bool encodeUtf8;

            /// <summary>Overriden method</summary>
            /// <returns>Filename in Zip</returns>
            public override string ToString()
            {
                return this.filenameInZip;
            }
        }

        #region Public fields
        /// <summary>True if UTF8 encoding for filename and comments, false if default (CP 437)</summary>
        public bool encodeUtf8 = false;
        /// <summary>Force deflate algotithm even if it inflates the stored file. Off by default.</summary>
        public bool forceDeflating = false;
        #endregion

        #region Private fields
        // List of files to store
        private List<ZipFileEntry> files = new List<ZipFileEntry>();
        // Filename of storage file
        private string fileName;
        // Stream object of storage file
        private Stream zipFileStream;
        // General comment
        private string comment = "";
        // Central dir image
        private byte[] centralDirImage = null;
        // Existing files in zip
        private ushort existingFiles = 0;
        // File access for Open method
        private FileAccess access;
        // Static CRC32 Table
        private static UInt32[] _crcTable = null;
        // Default filename encoder
        private static Encoding _defaultEncoding = Encoding.GetEncoding(437);
        #endregion

        #region Public methods
        // Static constructor. Just invoked once in order to create the CRC32 lookup table.
        static ZipStorer()
        {
            // Generate CRC32 table
            _crcTable = new UInt32[256];
            for (int i = 0; i < _crcTable.Length; i++)
            {
                UInt32 c = (UInt32)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((c & 1) != 0)
                        c = 3988292384 ^ (c >> 1);
                    else
                        c >>= 1;
                }
                _crcTable[i] = c;
            }
        }
        /// <summary>
        /// Method to create a new storage file
        /// </summary>
        /// <param name="filename">Full path of Zip file to create</param>
        /// <param name="comment">General comment for Zip file</param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipStorer create(string filename, string comment)
        {
            Stream stream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite);

            ZipStorer zip = create(stream, comment);
            zip.comment = comment;
            zip.fileName = filename;

            return zip;
        }
        /// <summary>
        /// Method to create a new zip storage in a stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="comment"></param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipStorer create(Stream stream, string comment)
        {
            ZipStorer zip = new ZipStorer();
            zip.comment = comment;
            zip.zipFileStream = stream;
            zip.access = FileAccess.Write;

            return zip;
        }
        /// <summary>
        /// Method to open an existing storage file
        /// </summary>
        /// <param name="filename">Full path of Zip file to open</param>
        /// <param name="access">File access mode as used in FileStream constructor</param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipStorer open(string filename, FileAccess access)
        {
            Stream stream = (Stream)new FileStream(filename, FileMode.Open, access == FileAccess.Read ? FileAccess.Read : FileAccess.ReadWrite);

            ZipStorer zip = open(stream, access);
            zip.fileName = filename;

            return zip;
        }
        /// <summary>
        /// Method to open an existing storage from stream
        /// </summary>
        /// <param name="stream">Already opened stream with zip contents</param>
        /// <param name="access">File access mode for stream operations</param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipStorer open(Stream stream, FileAccess access)
        {
            if (!stream.CanSeek && access != FileAccess.Read)
                throw new InvalidOperationException("Stream cannot seek");

            ZipStorer zip = new ZipStorer();
            //zip.FileName = _filename;
            zip.zipFileStream = stream;
            zip.access = access;

            if (zip.readFileInfo())
                return zip;

            throw new System.IO.InvalidDataException();
        }
        /// <summary>
        /// Add full contents of a file into the Zip storage
        /// </summary>
        /// <param name="method">Compression method</param>
        /// <param name="pathname">Full path of file to add to Zip storage</param>
        /// <param name="filenameInZip">Filename and path as desired in Zip directory</param>
        /// <param name="comment">Comment for stored file</param>        
        public void addFile(Compression method, string pathname, string filenameInZip, string comment)
        {
            if (access == FileAccess.Read)
                throw new InvalidOperationException("Writing is not alowed");

            FileStream stream = new FileStream(pathname, FileMode.Open, FileAccess.Read);
            addStream(method, filenameInZip, stream, File.GetLastWriteTime(pathname), comment);
            stream.Close();
        }
        /// <summary>
        /// Add full contents of a stream into the Zip storage
        /// </summary>
        /// <param name="method">Compression method</param>
        /// <param name="filenameInZip">Filename and path as desired in Zip directory</param>
        /// <param name="source">Stream object containing the data to store in Zip</param>
        /// <param name="modTime">Modification time of the data to store</param>
        /// <param name="comment">Comment for stored file</param>
        public void addStream(Compression method, string filenameInZip, Stream source, DateTime modTime, string comment)
        {
            if (access == FileAccess.Read)
                throw new InvalidOperationException("Writing is not alowed");

            long offset;
            if (this.files.Count==0)
                offset = 0;
            else
            {
                ZipFileEntry last = this.files[this.files.Count-1];
                offset = last.headerOffset + last.headerSize;
            }

            // Prepare the fileinfo
            ZipFileEntry zfe = new ZipFileEntry();
            zfe.method = method;
            zfe.encodeUtf8 = this.encodeUtf8;
            zfe.filenameInZip = normalizedFilename(filenameInZip);
            zfe.comment = (comment == null ? "" : comment);

            // Even though we write the header now, it will have to be rewritten, since we don't know compressed size or crc.
            zfe.crc32 = 0;  // to be updated later
            zfe.headerOffset = (uint)this.zipFileStream.Position;  // offset within file of the start of this local record
            zfe.modifyTime = modTime;

            // Write local header
            writeLocalHeader(ref zfe);
            zfe.fileOffset = (uint)this.zipFileStream.Position;

            // Write file to zip (store)
            store(ref zfe, source);
            source.Close();

            this.updateCrcAndSizes(ref zfe);

            files.Add(zfe);
        }
        /// <summary>
        /// Updates central directory (if pertinent) and close the Zip storage
        /// </summary>
        /// <remarks>This is a required step, unless automatic dispose is used</remarks>
        public void close()
        {
            if (this.access != FileAccess.Read)
            {
                uint centralOffset = (uint)this.zipFileStream.Position;
                uint centralSize = 0;

                if (this.centralDirImage != null)
                    this.zipFileStream.Write(centralDirImage, 0, centralDirImage.Length);

                for (int i = 0; i < files.Count; i++)
                {
                    long pos = this.zipFileStream.Position;
                    this.writeCentralDirRecord(files[i]);
                    centralSize += (uint)(this.zipFileStream.Position - pos);
                }

                if (this.centralDirImage != null)
                    this.writeEndRecord(centralSize + (uint)centralDirImage.Length, centralOffset);
                else
                    this.writeEndRecord(centralSize, centralOffset);
            }

            if (this.zipFileStream != null)
            {
                this.zipFileStream.Flush();
                this.zipFileStream.Dispose();
                this.zipFileStream = null;
            }
        }
        /// <summary>
        /// Read all the file records in the central directory 
        /// </summary>
        /// <returns>List of all entries in directory</returns>
        public List<ZipFileEntry> readCentralDir()
        {
            if (this.centralDirImage == null)
                throw new InvalidOperationException("Central directory currently does not exist");

            List<ZipFileEntry> result = new List<ZipFileEntry>();

            for (int pointer = 0; pointer < this.centralDirImage.Length; )
            {
                uint signature = BitConverter.ToUInt32(centralDirImage, pointer);
                if (signature != 0x02014b50)
                    break;

                bool encodeUtf8 = (BitConverter.ToUInt16(centralDirImage, pointer + 8) & 0x0800) != 0;
                ushort method = BitConverter.ToUInt16(centralDirImage, pointer + 10);
                uint modifyTime = BitConverter.ToUInt32(centralDirImage, pointer + 12);
                uint crc32 = BitConverter.ToUInt32(centralDirImage, pointer + 16);
                uint comprSize = BitConverter.ToUInt32(centralDirImage, pointer + 20);
                uint fileSize = BitConverter.ToUInt32(centralDirImage, pointer + 24);
                ushort filenameSize = BitConverter.ToUInt16(centralDirImage, pointer + 28);
                ushort extraSize = BitConverter.ToUInt16(centralDirImage, pointer + 30);
                ushort commentSize = BitConverter.ToUInt16(centralDirImage, pointer + 32);
                uint headerOffset = BitConverter.ToUInt32(centralDirImage, pointer + 42);
                uint headerSize = (uint)( 46 + filenameSize + extraSize + commentSize);

                Encoding encoder = encodeUtf8 ? Encoding.UTF8 : _defaultEncoding;

                ZipFileEntry zfe = new ZipFileEntry();
                zfe.method = (Compression)method;
                zfe.filenameInZip = encoder.GetString(centralDirImage, pointer + 46, filenameSize);
                zfe.fileOffset = getFileOffset(headerOffset);
                zfe.fileSize = fileSize;
                zfe.compressedSize = comprSize;
                zfe.headerOffset = headerOffset;
                zfe.headerSize = headerSize;
                zfe.crc32 = crc32;
                zfe.modifyTime = dosTimeToDateTime(modifyTime);
                if (commentSize > 0)
                    zfe.comment = encoder.GetString(centralDirImage, pointer + 46 + filenameSize + extraSize, commentSize);

                result.Add(zfe);
                pointer += (46 + filenameSize + extraSize + commentSize);
            }

            return result;
        }
        /// <summary>
        /// Copy the contents of a stored file into a physical file
        /// </summary>
        /// <param name="zfe">Entry information of file to extract</param>
        /// <param name="filename">Name of file to store uncompressed data</param>
        /// <returns>True if success, false if not.</returns>
        /// <remarks>Unique compression methods are Store and Deflate</remarks>
        public bool extractFile(ZipFileEntry zfe, string filename)
        {
            // Make sure the parent directory exist
            string path = System.IO.Path.GetDirectoryName(filename);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            // Check it is directory. If so, do nothing
            if (Directory.Exists(filename))
                return true;

            Stream output = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bool result = extractFile(zfe, output);
            if (result)
                output.Close();

            File.SetCreationTime(filename, zfe.modifyTime);
            File.SetLastWriteTime(filename, zfe.modifyTime);
            
            return result;
        }
        /// <summary>
        /// Copy the contents of a stored file into an opened stream
        /// </summary>
        /// <param name="zfe">Entry information of file to extract</param>
        /// <param name="stream">Stream to store the uncompressed data</param>
        /// <returns>True if success, false if not.</returns>
        /// <remarks>Unique compression methods are Store and Deflate</remarks>
        public bool extractFile(ZipFileEntry zfe, Stream stream)
        {
            if (!stream.CanWrite)
                throw new InvalidOperationException("Stream cannot be written");

            // check signature
            byte[] signature = new byte[4];
            this.zipFileStream.Seek(zfe.headerOffset, SeekOrigin.Begin);
            this.zipFileStream.Read(signature, 0, 4);
            if (BitConverter.ToUInt32(signature, 0) != 0x04034b50)
                return false;

            // Select input stream for inflating or just reading
            Stream inStream;
            if (zfe.method == Compression.Store)
                inStream = this.zipFileStream;
            else if (zfe.method == Compression.Deflate)
                inStream = new DeflateStream(this.zipFileStream, CompressionMode.Decompress, true);
            else
                return false;

            // Buffered copy
            byte[] buffer = new byte[16384];
            this.zipFileStream.Seek(zfe.fileOffset, SeekOrigin.Begin);
            uint bytesPending = zfe.fileSize;
            while (bytesPending > 0)
            {
                int bytesRead = inStream.Read(buffer, 0, (int)Math.Min(bytesPending, buffer.Length));
                stream.Write(buffer, 0, bytesRead);
                bytesPending -= (uint)bytesRead;
            }
            stream.Flush();

            if (zfe.method == Compression.Deflate)
                inStream.Dispose();
            return true;
        }
        /// <summary>
        /// Removes one of many files in storage. It creates a new Zip file.
        /// </summary>
        /// <param name="zip">Reference to the current Zip object</param>
        /// <param name="zfes">List of Entries to remove from storage</param>
        /// <returns>True if success, false if not</returns>
        /// <remarks>This method only works for storage of type FileStream</remarks>
        public static bool removeEntries(ref ZipStorer zip, List<ZipFileEntry> zfes)
        {
            if (!(zip.zipFileStream is FileStream))
                throw new InvalidOperationException("RemoveEntries is allowed just over streams of type FileStream");


            //Get full list of entries
            List<ZipFileEntry> fullList = zip.readCentralDir();

            //In order to delete we need to create a copy of the zip file excluding the selected items
            string tempZipName = Path.GetTempFileName();
            string tempEntryName = Path.GetTempFileName();

            try
            {
                ZipStorer tempZip = ZipStorer.create(tempZipName, string.Empty);

                foreach (ZipFileEntry zfe in fullList)
                {
                    if (!zfes.Contains(zfe))
                    {
                        if (zip.extractFile(zfe, tempEntryName))
                        {
                            tempZip.addFile(zfe.method, tempEntryName, zfe.filenameInZip, zfe.comment);
                        }
                    }
                }
                zip.close();
                tempZip.close();

                File.Delete(zip.fileName);
                File.Move(tempZipName, zip.fileName);

                zip = ZipStorer.open(zip.fileName, zip.access);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (File.Exists(tempZipName))
                    File.Delete(tempZipName);
                if (File.Exists(tempEntryName))
                    File.Delete(tempEntryName);
            }
            return true;
        }
        #endregion

        #region Private methods
        // Calculate the file offset by reading the corresponding local header
        private uint getFileOffset(uint headerOffset)
        {
            byte[] buffer = new byte[2];

            this.zipFileStream.Seek(headerOffset + 26, SeekOrigin.Begin);
            this.zipFileStream.Read(buffer, 0, 2);
            ushort filenameSize = BitConverter.ToUInt16(buffer, 0);
            this.zipFileStream.Read(buffer, 0, 2);
            ushort extraSize = BitConverter.ToUInt16(buffer, 0);

            return (uint)(30 + filenameSize + extraSize + headerOffset);
        }
        /* Local file header:
            local file header signature     4 bytes  (0x04034b50)
            version needed to extract       2 bytes
            general purpose bit flag        2 bytes
            compression method              2 bytes
            last mod file time              2 bytes
            last mod file date              2 bytes
            crc-32                          4 bytes
            compressed size                 4 bytes
            uncompressed size               4 bytes
            filename length                 2 bytes
            extra field length              2 bytes

            filename (variable size)
            extra field (variable size)
        */
        private void writeLocalHeader(ref ZipFileEntry zfe)
        {
            long pos = this.zipFileStream.Position;
            Encoding encoder = zfe.encodeUtf8 ? Encoding.UTF8 : _defaultEncoding;
            byte[] encodedFilename = encoder.GetBytes(zfe.filenameInZip);

            this.zipFileStream.Write(new byte[] { 80, 75, 3, 4, 20, 0}, 0, 6); // No extra header
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)(zfe.encodeUtf8 ? 0x0800 : 0)), 0, 2); // filename and comment encoding 
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)zfe.method), 0, 2);  // zipping method
            this.zipFileStream.Write(BitConverter.GetBytes(dateTimeToDosTime(zfe.modifyTime)), 0, 4); // zipping date and time
            this.zipFileStream.Write(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, 12); // unused CRC, un/compressed size, updated later
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)encodedFilename.Length), 0, 2); // filename length
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // extra length

            this.zipFileStream.Write(encodedFilename, 0, encodedFilename.Length);
            zfe.headerSize = (uint)(this.zipFileStream.Position - pos);
        }
        /* Central directory's File header:
            central file header signature   4 bytes  (0x02014b50)
            version made by                 2 bytes
            version needed to extract       2 bytes
            general purpose bit flag        2 bytes
            compression method              2 bytes
            last mod file time              2 bytes
            last mod file date              2 bytes
            crc-32                          4 bytes
            compressed size                 4 bytes
            uncompressed size               4 bytes
            filename length                 2 bytes
            extra field length              2 bytes
            file comment length             2 bytes
            disk number start               2 bytes
            internal file attributes        2 bytes
            external file attributes        4 bytes
            relative offset of local header 4 bytes

            filename (variable size)
            extra field (variable size)
            file comment (variable size)
        */
        private void writeCentralDirRecord(ZipFileEntry zfe)
        {
            Encoding encoder = zfe.encodeUtf8 ? Encoding.UTF8 : _defaultEncoding;
            byte[] encodedFilename = encoder.GetBytes(zfe.filenameInZip);
            byte[] encodedComment = encoder.GetBytes(zfe.comment);

            this.zipFileStream.Write(new byte[] { 80, 75, 1, 2, 23, 0xB, 20, 0 }, 0, 8);
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)(zfe.encodeUtf8 ? 0x0800 : 0)), 0, 2); // filename and comment encoding 
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)zfe.method), 0, 2);  // zipping method
            this.zipFileStream.Write(BitConverter.GetBytes(dateTimeToDosTime(zfe.modifyTime)), 0, 4);  // zipping date and time
            this.zipFileStream.Write(BitConverter.GetBytes(zfe.crc32), 0, 4); // file CRC
            this.zipFileStream.Write(BitConverter.GetBytes(zfe.compressedSize), 0, 4); // compressed file size
            this.zipFileStream.Write(BitConverter.GetBytes(zfe.fileSize), 0, 4); // uncompressed file size
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)encodedFilename.Length), 0, 2); // Filename in zip
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // extra length
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)encodedComment.Length), 0, 2);

            this.zipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // disk=0
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // file type: binary
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // Internal file attributes
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)0x8100), 0, 2); // External file attributes (normal/readable)
            this.zipFileStream.Write(BitConverter.GetBytes(zfe.headerOffset), 0, 4);  // Offset of header

            this.zipFileStream.Write(encodedFilename, 0, encodedFilename.Length);
            this.zipFileStream.Write(encodedComment, 0, encodedComment.Length);
        }
        /* End of central dir record:
            end of central dir signature    4 bytes  (0x06054b50)
            number of this disk             2 bytes
            number of the disk with the
            start of the central directory  2 bytes
            total number of entries in
            the central dir on this disk    2 bytes
            total number of entries in
            the central dir                 2 bytes
            size of the central directory   4 bytes
            offset of start of central
            directory with respect to
            the starting disk number        4 bytes
            zipfile comment length          2 bytes
            zipfile comment (variable size)
        */
        private void writeEndRecord(uint size, uint offset)
        {
            Encoding encoder = this.encodeUtf8 ? Encoding.UTF8 : _defaultEncoding;
            byte[] encodedComment = encoder.GetBytes(this.comment);

            this.zipFileStream.Write(new byte[] { 80, 75, 5, 6, 0, 0, 0, 0 }, 0, 8);
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)files.Count+existingFiles), 0, 2);
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)files.Count+existingFiles), 0, 2);
            this.zipFileStream.Write(BitConverter.GetBytes(size), 0, 4);
            this.zipFileStream.Write(BitConverter.GetBytes(offset), 0, 4);
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)encodedComment.Length), 0, 2);
            this.zipFileStream.Write(encodedComment, 0, encodedComment.Length);
        }
        // Copies all source file into storage file
        private void store(ref ZipFileEntry zfe, Stream source)
        {
            byte[] buffer = new byte[16384];
            int bytesRead;
            uint totalRead = 0;
            Stream outStream;

            long posStart = this.zipFileStream.Position;
            long sourceStart = source.Position;

            if (zfe.method == Compression.Store)
                outStream = this.zipFileStream;
            else
                outStream = new DeflateStream(this.zipFileStream, CompressionMode.Compress, true);

            zfe.crc32 = 0 ^ 0xffffffff;
            
            do
            {
                bytesRead = source.Read(buffer, 0, buffer.Length);
                totalRead += (uint)bytesRead;
                if (bytesRead > 0)
                {
                    outStream.Write(buffer, 0, bytesRead);

                    for (uint i = 0; i < bytesRead; i++)
                    {
                        zfe.crc32 = ZipStorer._crcTable[(zfe.crc32 ^ buffer[i]) & 0xFF] ^ (zfe.crc32 >> 8);
                    }
                }
            } while (bytesRead == buffer.Length);
            outStream.Flush();

            if (zfe.method == Compression.Deflate)
                outStream.Dispose();

            zfe.crc32 ^= 0xffffffff;
            zfe.fileSize = totalRead;
            zfe.compressedSize = (uint)(this.zipFileStream.Position - posStart);

            // Verify for real compression
            if (zfe.method == Compression.Deflate && !this.forceDeflating && source.CanSeek && zfe.compressedSize > zfe.fileSize)
            {
                // Start operation again with Store algorithm
                zfe.method = Compression.Store;
                this.zipFileStream.Position = posStart;
                this.zipFileStream.SetLength(posStart);
                source.Position = sourceStart;
                this.store(ref zfe, source);
            }
        }
        /* DOS Date and time:
            MS-DOS date. The date is a packed value with the following format. Bits Description 
                0-4 Day of the month (1–31) 
                5-8 Month (1 = January, 2 = February, and so on) 
                9-15 Year offset from 1980 (add 1980 to get actual year) 
            MS-DOS time. The time is a packed value with the following format. Bits Description 
                0-4 Second divided by 2 
                5-10 Minute (0–59) 
                11-15 Hour (0–23 on a 24-hour clock) 
        */
        private uint dateTimeToDosTime(DateTime dt)
        {
            return (uint)(
                (dt.Second / 2) | (dt.Minute << 5) | (dt.Hour << 11) | 
                (dt.Day<<16) | (dt.Month << 21) | ((dt.Year - 1980) << 25));
        }
        private DateTime dosTimeToDateTime(uint dt)
        {
            return new DateTime(
                (int)(dt >> 25) + 1980,
                (int)(dt >> 21) & 15,
                (int)(dt >> 16) & 31,
                (int)(dt >> 11) & 31,
                (int)(dt >> 5) & 63,
                (int)(dt & 31) * 2);
        }

        /* CRC32 algorithm
          The 'magic number' for the CRC is 0xdebb20e3.  
          The proper CRC pre and post conditioning
          is used, meaning that the CRC register is
          pre-conditioned with all ones (a starting value
          of 0xffffffff) and the value is post-conditioned by
          taking the one's complement of the CRC residual.
          If bit 3 of the general purpose flag is set, this
          field is set to zero in the local header and the correct
          value is put in the data descriptor and in the central
          directory.
        */
        private void updateCrcAndSizes(ref ZipFileEntry zfe)
        {
            long lastPos = this.zipFileStream.Position;  // remember position

            this.zipFileStream.Position = zfe.headerOffset + 8;
            this.zipFileStream.Write(BitConverter.GetBytes((ushort)zfe.method), 0, 2);  // zipping method

            this.zipFileStream.Position = zfe.headerOffset + 14;
            this.zipFileStream.Write(BitConverter.GetBytes(zfe.crc32), 0, 4);  // Update CRC
            this.zipFileStream.Write(BitConverter.GetBytes(zfe.compressedSize), 0, 4);  // Compressed size
            this.zipFileStream.Write(BitConverter.GetBytes(zfe.fileSize), 0, 4);  // Uncompressed size

            this.zipFileStream.Position = lastPos;  // restore position
        }
        // Replaces backslashes with slashes to store in zip header
        private string normalizedFilename(string _filename)
        {
            string filename = _filename.Replace('\\', '/');

            int pos = filename.IndexOf(':');
            if (pos >= 0)
                filename = filename.Remove(0, pos + 1);

            return filename.Trim('/');
        }
        // Reads the end-of-central-directory record
        private bool readFileInfo()
        {
            if (this.zipFileStream.Length < 22)
                return false;

            try
            {
                this.zipFileStream.Seek(-17, SeekOrigin.End);
                BinaryReader br = new BinaryReader(this.zipFileStream);
                do
                {
                    this.zipFileStream.Seek(-5, SeekOrigin.Current);
                    UInt32 sig = br.ReadUInt32();
                    if (sig == 0x06054b50)
                    {
                        this.zipFileStream.Seek(6, SeekOrigin.Current);

                        UInt16 entries = br.ReadUInt16();
                        Int32 centralSize = br.ReadInt32();
                        UInt32 centralDirOffset = br.ReadUInt32();
                        UInt16 commentSize = br.ReadUInt16();

                        // check if comment field is the very last data in file
                        if (this.zipFileStream.Position + commentSize != this.zipFileStream.Length)
                            return false;

                        // Copy entire central directory to a memory buffer
                        this.existingFiles = entries;
                        this.centralDirImage = new byte[centralSize];
                        this.zipFileStream.Seek(centralDirOffset, SeekOrigin.Begin);
                        this.zipFileStream.Read(this.centralDirImage, 0, centralSize);

                        // Leave the pointer at the begining of central dir, to append new files
                        this.zipFileStream.Seek(centralDirOffset, SeekOrigin.Begin);
                        return true;
                    }
                } while (this.zipFileStream.Position > 0);
            }
            catch { }

            return false;
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Closes the Zip file stream
        /// </summary>
        public void Dispose()
        {
            this.close();
        }
        #endregion
    }
}
