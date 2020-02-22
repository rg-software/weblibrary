// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WebLibrary
{
    class ArticleInfo
    {
        public enum ColumnType { Name, Type, Date, Favorite, Read }; // also defines the order of columns
        public static string StarTag => "S";
        public static string ReadTag => "R";
        private const char star_symbol = '\u2606';
        private const char check_symbol = '\u2713';

        internal struct Date : IComparable
        {
            public Date(DateTime dt)
            {
                mDateTime = dt;
            }

            public override string ToString()
            {
                return mDateTime.ToShortDateString();
            }

            public int CompareTo(object obj)
            {
                return mDateTime.CompareTo(((Date) obj).mDateTime);
            }

            private DateTime mDateTime;
        }
        public ArticleInfo(string fileName, DateTime creationDate)
        {
            var fileExtension = Path.GetExtension(fileName);
            if (fileExtension != null && fileExtension.StartsWith("."))
                fileExtension = fileExtension.Substring(1);

            FileName = fileName;
            ArticleName = Path.GetFileNameWithoutExtension(fileName);
            if (ArticleName.EndsWith("}") && ArticleName.Contains("{"))
            {
                // extract tags from file name
                int tagsIdx = ArticleName.LastIndexOf("{") + 1;
                var tags = ArticleName.Substring(tagsIdx, ArticleName.Length - tagsIdx - 1);
                Tags = new HashSet<string>(tags.Split(','));

                ArticleName = ArticleName.Substring(0, tagsIdx - 1);
            }

            ArticleType = fileExtension ?? "";
            CreationDate = new Date(creationDate);
        }

        public bool TaggedWith(string tag)
        {
            return Tags.Contains(tag);
        }

        private string formFileName(string name, string type, HashSet<string> tags)
        {
            var tagstring = String.Join(",", tags);
            if (tagstring.Length > 0)
                tagstring = "{" + tagstring + "}";

            return $"{name}{tagstring}.{type}";
        }

        public string FileNameWithNewName(string newName)
        {
            return formFileName(newName, ArticleType, Tags);
        }

        public string FileNameWithTagToggled(string tag)
        {
            var tags = new HashSet<string>(Tags);
            if (tags.Contains(tag))
                tags.Remove(tag);
            else
                tags.Add(tag);

            return formFileName(ArticleName, ArticleType, tags);
        }

        public IComparable GetField(ColumnType colType)
        {
            if (colType == ColumnType.Name) return ArticleName;
            if (colType == ColumnType.Type) return ArticleType;
            if (colType == ColumnType.Date) return CreationDate;
            if (colType == ColumnType.Favorite) return TaggedWith(StarTag) ? star_symbol : ' ';
            if (colType == ColumnType.Read) return TaggedWith(ReadTag) ? check_symbol : ' ';
            throw new Exception("Incorrect column type");
        }

        public string FileName { get; }
        public string ArticleName { get; }
        public string ArticleType { get; }
        public Date CreationDate { get; }
        public HashSet<string> Tags = new HashSet<string>();
    }
}
