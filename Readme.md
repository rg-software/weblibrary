# WebLibrary
The purpose of WebLibrary is to be a convenient offline browser/organizer of locally stored internet documents. The most typical use case is to read web pages, saved "for later reading". Naturally, this can be done merely with a web browser, but my experience shows that even a simple organizing system can be handy.

## Functionality
WebLibrary is still a the early stage of development, so the capabilities aren't rich yet, but sufficient for basic daily use.

![](weblibrary_screenshot.png)

These are the things you can do:

* Display any folder of your computer as a "Library".
* View individual saved documents in a browser window.
* Mark documents as "Favorite" or "Read".
* Sort documents by name, type, or favorite/read status.

WebLibrary supports keyboard shortcuts, so many basic operations can be perfomed quickly and without annoying mouse clicks. The content of the library is updated automatically, so when files appear or get deleted in the library folder, the main applicaton screen is updated accordingly.

## Content saving and synchronization
WebLibrary does not yet provide its own capabilities for saving web pages. For now, I recommend using a great browser extension [SingleFile](https://github.com/gildas-lormeau/SingleFile). To access your documents from different devices, simply place your library into a cloud-synchronized folder.

## Alternatives
There are other, much more function-rich systems like [TagSpaces](https://www.tagspaces.org/) or [myBase](http://www.wjjsoft.com/mybase.html). I tried using them several times on daily basis, but always had to face with problems like poor page rendering, lack of hotkeys or synchronization issues. However, your experience might be very different.

## Tech details
WebLibrary is written in C# and uses [CefSharp](https://github.com/cefsharp/CefSharp/) library to display HTML documents. Metainformation, such as "Favorite" and "Read" flags is encoded in file names (a file `doc.html` becomes `doc{R,S}.html` when it is marked as both read and favorite/starred). 

The project can be built with MS Visual Studio 2017. Open `Developer Command Prompt for VS 2017`, navigate to the project folder and run `BuildAndPack.bat`.
