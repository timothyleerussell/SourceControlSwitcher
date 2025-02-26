﻿// Copyright (c) 2013-2014 Francesco Pretto
// This file is subject to the MIT license

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using System.IO;
using Microsoft.VisualStudio.Shell;

namespace SourceControlSwitcher
{
    public partial class MainSite
    {
        public const string SVN_DIR = ".svn";
        public const string GIT_DIR = ".git";
        public const string MERCURIAL_DIR = ".hg";
        public const string SOURCEGEAR_VAULT_DIR = "_sgbak";

        public int OnAfterCloseSolution(object pUnkReserved)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterClosingChildren(IVsHierarchy pHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeLoadProject(IVsHierarchy pHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterMergeSolution(object pUnkReserved)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            //SetSCC(_DTE2.Solution.FullName);
            AppHelper.Output("OnAfterOpenSolution");
            return VSConstants.S_OK;
        }

        public int OnAfterOpeningChildren(IVsHierarchy pHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeCloseSolution(object pUnkReserved)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeClosingChildren(IVsHierarchy pHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeOpeningChildren(IVsHierarchy pHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterBackgroundSolutionLoadComplete()
        {
            return VSConstants.S_OK;
        }

        public int OnAfterLoadProjectBatch(bool fIsBackgroundIdleBatch)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeBackgroundSolutionLoadBegins()
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeLoadProjectBatch(bool fIsBackgroundIdleBatch)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeOpenSolution(string pszSolutionFilename)
        {
            AppHelper.Output("OnBeforeOpenSolution");
            _CurrentSolutionRcsType = RcsType.Unknown;
            SetSCC(pszSolutionFilename);

            return VSConstants.S_OK;
        }

        private static bool IsPerforce(DirectoryInfo directory)
        {
            const int ERROR_SUCCESS = 0;

            try
            {
                using (var process = new Process
                {
                    StartInfo =
                    {
                        WorkingDirectory = directory.FullName,
                        FileName = "p4.exe",
                        Arguments = "where",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                })
                {
                    return process.Start() && process.WaitForExit(10000) && process.ExitCode == ERROR_SUCCESS;
                }
            }
            catch
            {
                return false;
            }
        }

        public int OnQueryBackgroundLoadProjectBatch(out bool pfShouldDelayLoadToNextIdle)
        {
            pfShouldDelayLoadToNextIdle = false;
            return VSConstants.S_OK;
        }

        public RcsType SetSCC(string pszSolutionFilename)
        {
            DirectoryInfo solutionDir = new DirectoryInfo(Path.GetDirectoryName(pszSolutionFilename));
            DirectoryInfo currdir = solutionDir;
            var oldRcsType = _CurrentSolutionRcsType;
            ThreadHelper.ThrowIfNotOnUIThread();
            while (true)
            {
                if (Directory.Exists(Path.Combine(currdir.FullName, GIT_DIR)))
                {
                    if (_CurrentSolutionRcsType != RcsType.Git)
                    {
                        MainSite.RegisterPrimarySourceControlProvider(RcsType.Git);
                        _CurrentSolutionRcsType = RcsType.Git;
                    }
                    break;
                }

                if (Directory.Exists(Path.Combine(currdir.FullName, MERCURIAL_DIR)))
                {
                    if (_CurrentSolutionRcsType != RcsType.Mercurial)
                    {
                        MainSite.RegisterPrimarySourceControlProvider(RcsType.Mercurial);
                        _CurrentSolutionRcsType = RcsType.Mercurial;
                    }
                    break;
                }

                if (Directory.Exists(Path.Combine(currdir.FullName, SVN_DIR)))
                {
                    if (_CurrentSolutionRcsType != RcsType.Subversion)
                    {
                        MainSite.RegisterPrimarySourceControlProvider(RcsType.Subversion);
                        _CurrentSolutionRcsType = RcsType.Subversion;
                    }
                    break;
                }

                if(Directory.Exists(Path.Combine(currdir.FullName, SOURCEGEAR_VAULT_DIR)))
                {
                    if(_CurrentSolutionRcsType != RcsType.SourceGearVault)
                    {
                        MainSite.RegisterPrimarySourceControlProvider(RcsType.SourceGearVault);
                        _CurrentSolutionRcsType = RcsType.SourceGearVault;
                    }
                    break;
                }


                if (currdir.Parent == null)
                    break;

                currdir = currdir.Parent;
            }

            if (_CurrentSolutionRcsType == oldRcsType 
                && _CurrentSolutionRcsType != RcsType.Perforce 
                && IsPerforce(solutionDir))
            {
                MainSite.RegisterPrimarySourceControlProvider(RcsType.Perforce);
                _CurrentSolutionRcsType = RcsType.Perforce;
            }

            return _CurrentSolutionRcsType;
        }
    }
}
