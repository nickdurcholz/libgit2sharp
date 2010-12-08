﻿using System;
using System.Runtime.InteropServices;

namespace libgit2sharp.Wrapper
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct git_commit
    {
        public git_object commit;
        public ulong time;
        public git_vector parents;

        public IntPtr tree;
        public IntPtr author;
        public IntPtr committer;

        [MarshalAs(UnmanagedType.LPStr)]
        public string message;

        [MarshalAs(UnmanagedType.LPStr)]
        public string message_short;

        [MarshalAs(UnmanagedType.U1)]
        public bool full_parse;

        internal Commit Build()
        {
            Person commitAuthor = null;

            var gitAuthor = (git_person)Marshal.PtrToStructure(author, typeof(git_person));
            commitAuthor = gitAuthor.Build();

            var gitCommitter = (git_person)Marshal.PtrToStructure(committer, typeof(git_person));
            Person commitCommitter = gitCommitter.Build();

            var gitTree = (git_tree)Marshal.PtrToStructure(tree, typeof(git_tree));
            Tree commitTree = gitTree.Build();

            return new Commit(ObjectId.ToString(commit.id.id), commitAuthor, commitCommitter, EpochHelper.ToDateTimeOffset((int)time), message, message_short, commitTree);
        }
    }
}
