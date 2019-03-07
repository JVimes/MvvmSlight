MvvmSlight is a very small MVVM helper library for WPF, inspired by [MVVM Light][1] and chapter 7 of the ["WPF Cookbook"][2]. It reduces boilerplate and makes certain things easier to do.

The developer does not officially offer public support, although issue tickets might be read.

MvvmSlight is under the [MIT license][3].

### Documentation

Most documentation comes via the IntelliSense tool-tips. Here's an overview of the classes:

`PropertyChangedSource` is base class for view-model and model classes.

`Command`/`Command<T>` are a way to put command handlers in the view-model.

`Utils` has a property, `IsInDesignMode`, that tells if the code is running in the XAML designer.


[1]: https://github.com/lbugnion/mvvmlight
[2]: https://www.packtpub.com/application-development/windows-presentation-foundation-45-cookbook
[3]: LICENSE.txt