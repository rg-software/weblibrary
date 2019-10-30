// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CefSharp;

namespace WebLibrary
{
    public class ArticleList
    {
        public ArticleList(int sortByColumn, bool reverseSort, ListView view)
        {
            mView = view;
            SortByColumn = sortByColumn;
            ReverseSort = reverseSort;
        }

        public string FileName(int idx)
        {
            return mList[idx].ArticleFileName;
        }

        public void HandleColumnClick(int idx)
        {
            if (SortByColumn != idx)
                SortByColumn = idx;
            else
                ReverseSort = !ReverseSort;

            FillArticles(mLastPath);
        }

        public void FillArticles(string path)
        {
            mLastPath = path;
            mView.BeginUpdate();

            int[] colWidths = new int[mView.Columns.Count];
            for(int i = 0; i < colWidths.Length; ++i)
                colWidths[i] = mView.Columns[i].Width;
            InitializeColWidths(colWidths);

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            var files = dirInfo.GetFiles();

            foreach (FileInfo file in files)
                mList.Add(new ListElement(file.Name, file.CreationTime));

            mList.Sort(new LEComparer(SortByColumn, ReverseSort));

            foreach (var e in mList)
            {
                ListViewItem item = new ListViewItem(e.ArticleName);
                item.SubItems.Add(e.ArticleType);
                item.SubItems.Add(e.CreationDate.ToShortDateString());
                item.SubItems.Add(e.Starred ? new string(star_symbol, 1) : "");
                mView.Items.Add(item);
            }

            mView.EndUpdate();
        }

        private void SelectItemByFileName(string filename)
        {
            for (int i = 0; i < mList.Count; ++i)
                if (mList[i].ArticleFileName == filename)
                {
                    mView.SelectedIndices.Clear();
                    mView.SelectedIndices.Add(i);
                    return;
                }
        }

        public void ToggleFavorite(string path, int idx)    // $mm TODO: this list object should know its path...
        {
            var newFileName = mList[idx].MakeFileNameWithStar(!mList[idx].Starred);
            System.IO.File.Move(Path.Combine(path, mList[idx].ArticleFileName), 
                Path.Combine(path, newFileName));
            FillArticles(path);
            SelectItemByFileName(newFileName);
        }

        public void InitializeColWidths(int[] colwidths)
        {
            string[] colNames = {"Name", "Type", "Date", "Favorite"};
            mList.Clear();
            mView.Clear();

            for (int i = 0; i < colNames.Length; ++i)
            {
                var caption = colNames[i];
                if (SortByColumn == i)
                    caption += " " + (ReverseSort ? down_arrow : up_arrow);
                mView.Columns.Add(caption);
            }

            // $mm TODO: add "Read" and "Starred" columns
            // use {RS} tags in the name
            // show progress indicator
            for (int i = 0; i < colwidths.Length; ++i)
                mView.Columns[i].Width = colwidths[i];
        }

        private class LEComparer : IComparer<ListElement>
        {
            public LEComparer(int column, bool reverseSort)
            {
                mColumn = column;
                mReverseSort = reverseSort;
            }

            public int Compare(ListElement x, ListElement y)
            {
                var result = 0;
                if (mColumn == 0)
                    result = String.Compare(x.ArticleName, y.ArticleName, StringComparison.Ordinal);
                else if (mColumn == 1)
                    result = String.Compare(x.ArticleType, y.ArticleType, StringComparison.Ordinal);
                else if (mColumn == 2)
                    result = DateTime.Compare(x.CreationDate, y.CreationDate);
                else if (mColumn == 3)
                    result = x.Starred.CompareTo(y.Starred);
                return mReverseSort ? -result : result;
            }

            private int mColumn;
            private bool mReverseSort;
        }
        
        private struct ListElement
        {
            public ListElement(string fileName, DateTime creationDate)
            {
                var fileExtension = Path.GetExtension(fileName);
                if (fileExtension != null && fileExtension.StartsWith("."))
                    fileExtension = fileExtension.Substring(1);

                ArticleFileName = fileName;
                ArticleName = Path.GetFileNameWithoutExtension(fileName);
                Starred = false;
                if (ArticleName.EndsWith("}") && ArticleName.Contains("{"))
                {
                    int tagsIdx = ArticleName.LastIndexOf("{");
                    var tags = ArticleName.Substring(tagsIdx);
                    Starred = tags.Contains("S");
                    ArticleName = ArticleName.Substring(0, tagsIdx);
                }

                ArticleType = fileExtension ?? "";
                CreationDate = creationDate;
            }

            public string MakeFileNameWithStar(bool star)
            {
                string starstr = star ? "S" : "";
                return $"{ArticleName}{{{starstr}}}.{ArticleType}";
            }

            public string ArticleFileName;
            public string ArticleName;
            public string ArticleType;
            public bool Starred;
            public DateTime CreationDate;
        }

        private const char up_arrow = '\u2191';
        private const char down_arrow = '\u2193';
        private const char star_symbol = '\u2606';
        // checkmark: U+2713
        
        private string mLastPath;
        private ListView mView;
        private List<ListElement> mList = new List<ListElement>();
        public int SortByColumn { get; private set; }
        public bool ReverseSort { get; private set; }
    }
}
