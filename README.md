# Source Control Switcher
# Forked to add SourceGear Vault support

![Source Control Switcher Logo](https://www.ryadel.com/wp-content/uploads/2019/10/Source-Control-Switcher-logo-368x200.png)

Automatically sets the Source Control Provider according to the one 
used by the opened Visual Studio project.

All you need to do is to set your favourite defaults using the 
extension's dedicated ***Source Control Switcher*** option tab, 
which will be added to your Visual Studio 
**Tools** -> **Options** panel upon install, as in the screenshot below:

![Source Control Switcher - Options Screenshot](https://www.ryadel.com/wp-content/uploads/2019/10/ss-01-1.png)

Once done, your favourite Source Control Provider will be automatically set everytime you open an existing project or solution.

Alternatively, you can use the **Default** feature to let the extension automatically pick 
a suitable Source Control Client among those installed on your system. 
This is often the best choice if you've only installed a single client/extension for each Source Control Provider.

Supported Source Control Clients (and their provider) are:

 * **AnkhSVN** *(Subversion)*
 * **VisualSVN** *(Subversion)*
 * **VisualSVN 2019** *(Subversion)*
 * **Easy Git Integration Tools (EZ-GIT)** *(Git)*
 * **Git Source Control Provider** *(Git)*
 * **HgSccPackage** *(Mercurial)*
 * **VisualHG** *(Mercurial)*
 * **VSHG** *(Mercurial)*
 * **P4VS** *(Helix)*
 * **SourceGear Vault** *(VSS replacement)*

More providers can be added, provided they are regular source control providers 
and there exists an easy way to detect
proper RCS type by checking file or directories presence
starting from solution root directory.

Each provider (Subversion, Git, Mercurial or Helix) can be configured to either load a specific client 
(among those supported) or to get the first installed one found (Default).

The extension is fully compatible with VS2015, VS2017, VS2019 and VS2022.

License is MIT.

This extension is strongly based to [SccAutoSwitcher](https://github.com/ceztko/SccAutoSwitcher) by *Francesco Pretto*, 
which sadly seems to be no longer updated 
and lack some important features such as VS2019/VS2022 support and async loading support
(which led me to create this project).

## Useful References
 * [**Source Control Switcher** official page](https://www.ryadel.com/en/portfolio/source-control-switcher/)
 * [**Source Control Switcher** on GitHub](https://github.com/Darkseal/SourceControlSwitcher/)
 * [**Source Control Switcher** on Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=Ryadel.SourceControlSwitcher)
