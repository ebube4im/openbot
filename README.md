# OpenBot
OpenBot is an open-source bot that is a community project. Help this grow by adding your code!
Since it's a community project, you can help by adding your code!

## Basic instructions on how to contribute:

How to add a command:
```c#
// in the "else if (m.Type == "say")"
else if (m.GetString(1) == "!mycommand")
{
    con.Send("[OpenBot] My command, oh no!");
}
```

This is it! You should've already contributed this project. Now you can test your own feature.

## How to rebuild/clone the project

Start a WinForms project. You can replace your own Form1.cs files with my files and implement a feature you want.
This is it! You have cloned the project.
