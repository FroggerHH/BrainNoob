# BrainNoob

Версия на [Русском](README_RU.md) 

BrainNoob is a programming language based on BrainFuck.
It expands BrainFuck's capabilities by adding new features that make life easier for developers.

### Main Features

Any non-special characters can be used in the code. This is necessary for method names.

Like TypeScript, BrainNoob turns into an ordinary BrainFuck.

BrainNoob allows you to:

- declare re-executable code snippets - methods;
- automatically clear memory cells;

### TODO

- splitting the code into files
- built-in libraries for character input/output

### Syntax

Methods must be declared at the beginning of the file.

The method body is highlighted using the method operator at the beginning and at the end. Example:

```bn
$h$  ++++++++  [>K+++++++++K<-]>.<        $h$
```

The method name is `h`, the body - `++++++++  [>K+++++++++K<-]>.<`

The method is called using a single method operator: `$h$`.


### Examples
There is a working script in the file `code.bn` in BrainNoob to display the phrase `Hello world` on the screen.