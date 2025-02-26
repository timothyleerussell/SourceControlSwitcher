﻿using System;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SourceControlSwitcher
{
    public class SwitcherOptions : DialogPage
    {
        [DisplayName("Git Provider")]
        [Description("Select the Git Source Control Provider to use.")]
        [Category("Source Control Providers")]
        public GitSccProvider GitProvider
        {
            get { return MainSite.GetGitSccProvider(); }
            set
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                MainSite.SetGitSccProvider(value); 
            }
        }

        [DisplayName("Subversion Provider")]
        [Description("Select the SVN Source Control Provider to use.")]
        [Category("Source Control Providers")]
        public SubversionSccProvider SubversionProvider
        {
            get { return MainSite.GetSubversionSccProvider(); }
            set 
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                MainSite.SetSubversionSccProvider(value); 
            }
        }

        [DisplayName("Mercurial Provider")]
        [Description("Select the HG/Mercurial Source Control Provider to use.")]
        [Category("Source Control Providers")]
        public MercurialSccProvider MercurialProvider
        {
            get { return MainSite.GetMercurialSccProvider(); }
            set 
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                MainSite.SetMercurialSccProvider(value); 
            }
        }

        [DisplayName("Perforce Provider")]
        [Description("Select the Helix/Perforce Source Control Provider to use.")]
        [Category("Source Control Providers")]
        public PerforceSccProvider PerforceProvider
        {
            get { return MainSite.GetPerforceSccProvider(); }
            set 
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                MainSite.SetPerforceSccProvider(value); 
            }
        }

        [DisplayName("SourceGear Vault Provider")]
        [Description("Select the SourceGear Vault Provider to use.")]
        [Category("Source Control Providers")]
        public SourceGearVaultSccProvider SourceGearVaultProvider
        {
            get { return MainSite.GetSourceGearVaultSccProvider(); }
            set
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                MainSite.SetSourceGearVaultSccProvider(value);
            }
        }
    }

    [Serializable]
    public enum GitSccProvider
    {
        Default = 0,

        [Description("Easy Git Integration Tools (EZ-GIT)")]
        [Display(Name = "Easy Git Integration Tools (EZ-GIT)")]
        [LocDisplayName("Easy Git Integration Tools (EZ-GIT)")]
        EasyGitIntegrationTools,

        [Description("Visual Studio Tools for Git")]
        [Display(Name = "Visual Studio Tools for Git")]
        [LocDisplayName("Visual Studio Tools for Git")]
        VisualStudioToolsForGit,

        Disabled
    }

    public enum SubversionSccProvider
    {
        Default = 0,

        [Description("AnkhSVN")]
        [Display(Name = "AnkhSVN")]
        [LocDisplayName("AnkhSVN")]
        AnkhSVN,

        [Description("VisualSVN")]
        [Display(Name = "VisualSVN")]
        [LocDisplayName("VisualSVN")]
        VisualSVN,

        Disabled
    }

    public enum MercurialSccProvider
    {
        Default = 0,

        [Description("HgSccPackage")]
        [Display(Name = "HgSccPackage")]
        [LocDisplayName("HgSccPackage")]
        HgSccPackage,

        [Description("VisualHG")]
        [Display(Name = "VisualHG")]
        [LocDisplayName("VisualHG")]
        VisualHG,

        [Description("VSHG")]
        [Display(Name = "VSHG")]
        [LocDisplayName("VSHG")]
        VSHG,

        Disabled
    }

    public enum PerforceSccProvider
    {
        Default = 0,

        [Description("P4VS")]
        [Display(Name = "P4VS")]
        [LocDisplayName("P4VS")]
        P4VS,

        Disabled
    }

    public enum SourceGearVaultSccProvider
    {
        Default = 0,

        [Description("SourceGear Vault Standard")]
        [Display(Name = "SourceGear Vault Standard")]
        [LocDisplayName("SourceGear Vault Standard")]
        SourceGearVaultStandard,

        [Description("SourceGear Vault Professional")]
        [Display(Name = "SourceGear Vault Professional")]
        [LocDisplayName("SourceGear Vault Professional")]
        SourceGearVaultProfessional,

        Disabled
    }
}
