﻿# BrainNoob

[English](README.md) version

BrainNoob — это язык программирования, основанный на BrainFuck.
Он расширяет возможности BrainFuck, добавляя новые функции, упрощающие жизнь разработчикам.

### Основные особенности

В коде могут быть использованы любые не специальные символы. Это необходимо для имён методов.

Как и TypeScript, BrainNoob превращается в обыкновенный BrainFuck.

BrainNoob позволяет:

- объявлять переисполняемые фрагменты кода - методы;
- автоматически очищать ячейки памяти;

### TODO

- разделение кода по файлам
- встроенные библиотеки для ввода/вывода символов

### Синтаксис

Методы должны быть объявлены в начале файла.

Тело метода выделяется при помощи оператора метода в начале и в конце. Пример:

```bn
$h$  ++++++++  [>K+++++++++K<-]>.<        $h$
```

Имя метода - `h`, тело - `++++++++  [>K+++++++++K<-]>.<`

ИВызов метода производится при помощи одинарного оператора метода: `$h$`.


### Примеры
В файле `code.bn` написан рабочий скрипт на BrainNoob для вывода на экран фразы `Hello world`.