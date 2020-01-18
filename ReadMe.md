MvvmSlight is a very small MVVM helper library for WPF, inspired by [MVVM Light][1] and chapter 7 of the ["WPF Cookbook"][2]. It reduces boilerplate and makes certain things easier to do.

Most documentation comes via IntelliSense tool-tips. Here's an overview of the classes:

* `PropertyChangedSource` is a base class for view-model and model classes.
* `Command`/`Command<T>` are a way to put command handlers in the view-model.
* `Utils` has a property, `IsInDesignMode`, that tells if the code is running in the XAML designer.

MvvmSlight is under the [MIT No Attribution license][3].


[1]: https://github.com/lbugnion/mvvmlight
[2]: https://www.packtpub.com/application-development/windows-presentation-foundation-45-cookbook
[3]: LICENSE.txt