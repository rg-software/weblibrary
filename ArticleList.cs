// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WebLibrary
{
    class ArticleList
    {
        private static Dictionary<ArticleInfo.ColumnType, string> ColumnNames = 
                                new Dictionary<ArticleInfo.ColumnType, string>(){
                                    { ArticleInfo.ColumnType.Name, "Name"},
                                    { ArticleInfo.ColumnType.Type, "Type"},
                                    { ArticleInfo.ColumnType.Date, "Date"},
                                    { ArticleInfo.ColumnType.Favorite, "Favorite"},
                                    { ArticleInfo.ColumnType.Read, "Read" }};

        public ArticleList(int sortByColumn, bool reverseSort, ListView view)
        {
            mView = view;
            SortByColumn = sortByColumn;
            ReverseSort = reverseSort;
        }

        public void HandleColumnClick(int idx)
        {
            if (SortByColumn != idx)
            {
                ReverseSort = false;
                SortByColumn = idx;
            }
            else
                ReverseSort = !ReverseSort;

            FillArticles(mCurrentPath);
        }

        private ListViewItem makeListViewItem(ArticleInfo e)
        {
            ListViewItem item = new ListViewItem(e.GetField(0).ToString());
            for (int i = 1; i < ColumnNames.Count; ++i)
                item.SubItems.Add(e.GetField((ArticleInfo.ColumnType)i).ToString());
            return item;
        }

        public void FillArticles(string path)
        {
            mCurrentPath = path;
            mView.BeginUpdate();

            int[] colWidths = new int[mView.Columns.Count];
            for(int i = 0; i < colWidths.Length; ++i)
                colWidths[i] = mView.Columns[i].Width;
            InitializeColWidths(colWidths);

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            foreach (FileInfo file in dirInfo.GetFiles())
                mList.Add(new ArticleInfo(file.Name, file.CreationTime));

            mList.Sort(new LEComparer(SortByColumn, ReverseSort));

            foreach (var e in mList)
                mView.Items.Add(makeListViewItem(e));

            mView.EndUpdate();
        }

        private void selectItemByFileName(string filename)
        {
            for (int i = 0; i < mList.Count; ++i)
                if (mList[i].FileName == filename)
                {
                    mView.SelectedIndices.Clear();
                    mView.SelectedIndices.Add(i);
                    return;
                }
        }

        public string GetFullPath(int idx)
        {
            return Path.Combine(mCurrentPath, mList[idx].FileName);
        }

        private void toggleTag(int idx, string tag)
        {
            var newFileName = mList[idx].FileNameWithTagToggled(tag);
            File.Move(Path.Combine(mCurrentPath, mList[idx].FileName), Path.Combine(mCurrentPath, newFileName));
            FillArticles(mCurrentPath);
            selectItemByFileName(newFileName);
        }

        public void ToggleRead(int idx)
        {
            toggleTag(idx, ArticleInfo.ReadTag);
        }

        public void ToggleFavorite(int idx)
        {
            toggleTag(idx, ArticleInfo.StarTag);
        }

        public void InitializeColWidths(int[] colwidths)
        {
            mList.Clear();
            mView.Clear();

            for (int i = 0; i < ColumnNames.Count; ++i)
            {
                string caption = ColumnNames[(ArticleInfo.ColumnType)i];
                if (SortByColumn == i)
                    caption += " " + (ReverseSort ? down_arrow : up_arrow);
                mView.Columns.Add(caption);
            }

            for (int i = 0; i < colwidths.Length; ++i)
                mView.Columns[i].Width = colwidths[i];
        }

        private class LEComparer : IComparer<ArticleInfo>
        {
            public LEComparer(int column, bool reverseSort)
            {
                mColumn = (ArticleInfo.ColumnType)column;
                mReverseSort = reverseSort;
            }
            
            public int Compare(ArticleInfo x, ArticleInfo y)
            {
                var result = x.GetField(mColumn).CompareTo(y.GetField(mColumn));
                return mReverseSort ? -result : result;
            }

            private ArticleInfo.ColumnType mColumn;
            private bool mReverseSort;
        }

        private const char up_arrow = '\u2191';
        private const char down_arrow = '\u2193';

        private string mCurrentPath;
        private ListView mView;
        private List<ArticleInfo> mList = new List<ArticleInfo>();

        public int SortByColumn { get; private set; }
        public bool ReverseSort { get; private set; }
    }
}
