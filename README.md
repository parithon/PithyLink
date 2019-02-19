# Pithy Link [![Open Source Love](https://badges.frapsoft.com/os/v1/open-source.svg?v=103)](https://github.com/ellerbrock/open-source-badges/) [![MIT Licence](https://badges.frapsoft.com/os/mit/mit.svg?v=103)](https://opensource.org/licenses/mit-license.php)

Master Branch | Dev Branch
------ | ---
[![Build Status](https://dev.azure.com/parithon/PithyLink/_apis/build/status/parithon.PithyLink?branchName=master)](https://dev.azure.com/parithon/PithyLink/_build/latest?definitionId=47&branchName=master)|[![Build Status](https://dev.azure.com/parithon/PithyLink/_apis/build/status/parithon.PithyLink?branchName=dev)](https://dev.azure.com/parithon/PithyLink/_build/latest?definitionId=47&branchName=dev)


## Preview Site

https://pithylink.azurewebsites.net

## Change Log

[CHANGES.md](CHANGES.md)

## Tools

### Visual Studio 2017 (15.9.7)

### Visual Studio Code 1.31.1

### Azure Cosmos DB Emulator for local development

We store the shortened urls in a NoSQL database:

[Use the Azure Cosmos DB Emulator for local development and testing](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator)

- [Binaries](https://aka.ms/cosmosdb-emulator)
- [Docker](https://hub.docker.com/r/microsoft/azure-cosmosdb-emulator/)

## Contributing

When contributing to this repository, please first discuss the changes you wish to make with the owners and maintainers by generating a 'Draft' Pull Request (PR).

### Pull Request Process

We welcome new contributors. This section will guide you through the contribution process.

#### 1. Step 1: Fork

Fork PithyLink [on GitHub](https://github.com/parithon/PithyLink) and checkout your local copy. We currently use the 'dev' branch as our default branch.

##### Using SSH

```
> git clone git@github.com:your-github-username/PithyLink.git
> cd PithyLink
> git remote add upstream git://github.com/parithon/Pithylink.git
```

##### Using HTTPS

```
> git clone https://github.com/your-github-username/PithyLink.git
> cd PithyLink
> git remote add upstream https://github.com/parithon/PithyLink.git
```

#### 2. Step 2: Branch

Create a feature branch for your code and start coding... Keeping in mind we use the following branch prefix' to better understand the new branches role:

Prefix | Purpose
--- | -------
feature_ | New Feature
bugfix_ | Fix a bug

```
> git checkout -b feature_MyNewFeature -t upstream/dev
```

#### 3. Step 3: Commit

**Writing good commit messages in important.** A commit message should describe what changed, why, and reference issues (if any). Follow these guidelines when writing your message:

1. The first line should be around 50-characters or less and contain a short description of the change.
2. Keep the second line blank.
3. Wrap all other lines at 72-characters.
4. Inclue ```Closes #N``` where *N* is the issue number the commit addresses, if any.

The first line must be meaningful as it's what people see when they run ```git shortlog``` or ```git log --online```.

Although we don't require it right now, it is a good idea to sign your commits prior to publishing them to GitHub. For instructions on how to accomplish this following these instructions:

- [Checking for existing GPG keys](https://help.github.com/articles/checking-for-existing-gpg-keys)
- [Generating a new GPG key](https://help.github.com/articles/generating-a-new-gpg-key/)
- [Adding a new GPG key to your GitHub account](https://help.github.com/articles/adding-a-new-gpg-key-to-your-github-account/)
- [Telling Git about your GPG key](https://help.github.com/articles/telling-git-about-your-gpg-key)
- [Associating an email with your GPG key](https://help.github.com/articles/associating-an-email-with-your-gpg-key)
- [Signing commits using GPG](https://help.github.com/articles/signing-commits-using-gpg/)

#### 4. Step 4: Stay up to date

Periodically you will want to pull in the latest changes from the upstream repository. This will minimize the number of merge conflicts during your PR; rebasing your branch will help keep your PR just about your changes.

```
> git fetch upstream
> git pull --rebase
```

**Note**: If a conflict comes up during a rebase, because of the way rebase works, your changes will be on the *remote* side of the conflict while the pulled-in commits are on the *local* site. This occurs because Git will automatically rollback your commit history until it matches the last pulled in commits from the remote branch. Then it will start applying the pulled in branch' commits and finally applying your commits last.

#### 5. Step 5: Push

You're almost ready to create your PR! Push the entire branch to your forked copy of the repository.

```
> git push -u origin feature_MyNewFeature
```

#### 6. Step 6: Start your Pull Request

Go to your forked repository: https://github.com/your-github-username/PithyLink. You will see a *Pull Request* button. Click it!

Ensure that you are generating a Pull Request to the *dev* branch of our repository and fill out the form. Click submit!

### Other Items of Note

1. Ensure any install or build dependencies are removed before the end of the layer when doing a build. If you notice a *build* folder is included with the source code, start an Issue to have the folder added to the *.gitignore* document.
2. Update the [CHANGES.md](CHANGES.md) with details of changes to the interface.ncludes new environment variables, exposed ports, useful file locations, etc.
3. Please submit all PRs to the *dev* branch unless you are working on a formal project, in which case you should submit the PR to that project-specific branch.
4. Project maintainers may merge the PR.

## Code of Conduct

We have a code of conduct you can read [here](CODE-OF-CONDUCT.md).

In short,  we as contributors and maintainers pledge to making participation in our project and our community a harassment-free experience for everyone.
