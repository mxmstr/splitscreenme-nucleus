# Nucleus Coop Contributing Guide  
Want to help out with the project? We'd love it! There are lots of different ways that you can help, whether it is developing game handlers, contributing to the project codebase, or simply a donation - all are appreciated.

## Communication
We would love your help! One of the most important parts of that is working with the Nucleus Coop team itself! Connect with other developers on the project to see how you can best help out!

### Join Discord!
The "developers" channel on the [official Discord Server](https://discord.gg/QDUt8HpCvr) is a great place to get started! Introduce yourself! Chat with the current project developers about what you would like to work on! This is a great way to get to know those working on the project and can also help guide your development contributions, coordinate work better, etc.

### Tips

* **Follow the Golden Rule** - Always be respectful, patient, and courteous! Remember, everyone working on this project is doing so with their own free time.

* **Relentless Communication** - Let folks know what you're working on and give status updates when you can. If you get overwhelmed or have to bow out of what you're working on, no worries! Life happens! Just try to keep other developers in the loop so they know what to expect. If you are actively working on a specific issue, it is very helpful to other prospective developers if you leave a comment stating as such on the issue post. Just make sure to add a follow-up comment if that changes!

* **Avoid Duplication** - It's always a good idea to check in with the other developers before you get invested in an idea, as the current GitHub "master" branch may not have had all relevant changes pushed yet. No sense working on something that may be irrelevant!

* **Commitment** - If you plan to take on a particular issue, and have communicated this to the team, please take the commitment seriously. Please don't take on more than you can handle. This way, others with the bandwidth are free to pursue the issue without worrying about whether they might be duplicating labor. If you start working on something and have to back out for any reason, that is fine and totally understandable! Just communicate as soon as possible when your ability to commit changes! If you are working on a specific issue, the issue post itself is a great way to leave status comments that won't get lost in chat scroll.

* **Focus** - Ambitious goals are great! That said, even the simplest coding task can find a way to snowball in complexity _very_ quickly. If you are new to this project (_especially_ if you are new to working on an open source project), try to start with small, discrete issues. Broader contributions take _a lot_ more work to finish and are more likely to lead to messy code-base conflicts. When at all possible, try to narrow the scope to a single, _specific_ issue.

* **Clear Language** - Try to keep technical correspondence as clear as possible and avoid overusing colloquialisms. One of the most awesome things about GitHub is that it is easy for anyone, worldwide, to contribute to an open-source project! As such, please be mindful of potential language barriers between developers. Try to keep things clear and simple, when possible.

## Game Handlers
If you’d like to expand the list of supported games (or improve an existing game’s support), check out the detailed [Handler Development Guide](https://www.splitscreen.me/docs/create-handlers)!

## Issue Contribution
If you encounter an issue while using Nucleus Coop, or have an idea for a new feature, you can create a [GitHub “Issue”](https://github.com/SplitScreen-Me/splitscreenme-nucleus/issues) using the “New Issue” button on the Issue page. This will take you to a selection page where you can choose the most relevant template for your issue.

In general, when submitting a new issue:

1.	**Avoid Duplicates**: Do a quick search to see if a similar issue already exists, to avoid redundancy and cluttering up issues.

2.	**Stick to a Single Issue**: Make sure that you aren’t combining multiple issues into a single ticket. If you have multiple issues (or are looking at a very high-level issue that has multiple sub-issues), make new GitHub issues for each. This makes it easier to track issue resolution and portion off tasks into more actionable chunks.

3.	**Note the Version and your Environment**: When creating the issue, make sure that you include details on which version of Nucleus Coop or a Handler that you are working with. This can help identify when issues first arise and identify whether they have already been resolved in newer versions. Including "environment" information (operating system, system hardware, controllers used, etc.) can help further isolate issues, in case they are only triggered by certain hardware setups.

4.	**Reproduction**: When filing a bug report, if at all possible, see if you can reproduce the bug and provide detailed steps as to how you did it. This is immensely helpful in narrowing down where problematic code may be triggering.

5.	**Include Logs**: If at all possible, please include the text from log files when encountering a crash or other weird issue. The “New Issue” bug form itself has more details on how to do this.

6.	**Security**: If you identify a security issue in the code of Nucleus Coop, please do not post a public issue identifying the security threat. This potentially highlights the issue for malicious users to take advantage of the vulnerability. Instead, please reach out to [active project maintainers](https://github.com/SplitScreen-Me/splitscreenme-nucleus?tab=readme-ov-file#credits) directly.

### Log Files and Bug Reporting
The SplitScreen.Me FAQ has a great article that discusses [Bug Reporting](https://www.splitscreen.me/docs/faq/#18--where-can-i-report-a-bugissue) in more detail, including how to enable and view log files.

## Working on the Source Code
If you would like to contribute to the codebase of Nucleus Coop as a developer, we would love to have you! Here are some helpful guidelines to make the process go as smoothly as possible!

### The Process
1.	**Fork the master branch** and clone it into your own development environment. As of right now, the “master” branch is the primary, active branch of the project.

2.	**Work on your changes**. Make sure to check for updates to the master branch before you create a pull request to avoid unnecessary code conflicts.

3.	**Please adhere to the code standards** when writing your new code.

4.	 [**Create a pull request**](https://github.com/SplitScreen-Me/splitscreenme-nucleus/pulls) once you are done with your changes! Please verify that your fork is caught up with the master branch beforehand.

5.	**Your code will be reviewed**, and changes may potentially be requested of your pull request. If this happens, work to address any issues identified and then update the pull request! Repeat as necessary until your code is accepted.

6.	**Done!** Once your code is reviewed and accepted, it will be a part of the project! Congratulations!

### Project Branches
As previously mentioned, the "master" branch of the project is the primary development branch of Nucleus Coop. Generally, you should be safe to assume that this is the current branch for everyone to work with. If you ever see other branches with recent updates and aren't sure which branch you should be forking or working from, just ask!

### Merging to the Correct Branch
**Warning** - When you submit a Pull Request, make sure that you have the correct "merge" target set. The "base repository" is the target _destination_ for your changes, while the "head repository" is the _source_ of the changes (i.e. your forked repository).

The current iteration of the project has gone through several forks in its history, and when you go to submit a pull request, GitHub may default the merge target to a _different_ fork.

**After clicking "New Pull Request" on the Pull Request page, make sure that your base and head repositories are correct before continuing.**

Make sure that for the "base" repository, the "master" branch is selected. If you are merging to a different branch for some reason, merge to that branch instead. For the head, the branch should be whatever branch you would like to merge from in your own repository fork.

_The **Comparing Changes** selectors must display as follows for the Pull Request to work correctly._

**Base Repository**
>base repository: SplitScreen-Me/splitscreenme-nucleus

If Github defaults the head to SplitScreen-Me/splitscreenme-nucleus, you will need to change the head _first_. Otherwise, GitHub may see you as trying to merge from within the same project and drop the option to select target/destination forks (requiring you to backtrack and start base/head selection over again).
**Base**
>base:master

Use "master" unless you are intentionally merging with a different branch for some reason (in which case, use the target branch instead).

**Head Repository**
>head repository: YourUserName/ForkRepoName

**Compare**
>compare: YourSourceBranch

**Caution** - There may be some delay or browser hitching while selecting repositories while the repository selector populates selection options.

### How to Compile for Testing
If you plan to fork the project and test your own changes, you will need to compile Nucleus Coop from code yourself. Please see the detailed [Compiling Guide](https://nucleus-coop.github.io/docs/compilation/) for specific details on how to compile the project into a runnable executable file.

**Note:** After cloning your fork to your local development environment, make sure to run the following git command locally to make sure that all the submodule dependencies are downloaded:
>git submodule update --init --recursive 

This is covered in the linked tutorial but is worth repeating.

**Warning** - Nucleus Coop will **not** run correctly without these submodules.

### Code Standards
* When in doubt, structure your code to err on the side of readability!
* Comment large blocks of abstract code with concise descriptions.
* Please write C# code following [standard conventions](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/capitalization-conventions). 

## Donations
If you would like to contribute financially to support the project, check out the links under the "Sponsor this Project" section of the [repository homepage](https://github.com/SplitScreen-Me/splitscreenme-nucleus). Donations are always appreciated, but completely voluntary!

# Other Resources
If you can't find the answers you are looking for here, you may want to take a look at some of the other project documentation.
* The [Official FAQ](https://www.splitscreen.me/docs/faq) answers a lot of common questions about the project!
* The [Documents](https://nucleus-coop.github.io/docs/quickstart/) section of the Nucleus Coop website is a good place to get started! It has helpful links to a lot of key setup resources. It also has detailed instructions for compiling Nucleus Coop and working with Proto Input.

If you still can't find an answer to your question, feel free to ask around on the support or developer channels of the [Nucleus Coop Discord Server](https://discord.gg/QDUt8HpCvr), depending on the nature of what you are looking for.
