## Техническоее задание
Тестовое задание на позицию разработчика в СПБ Биржу.
Нужно дописать в  проекте методы, у которых нет реализации.

В идеале нужно, что бы все тесты успешно проходили.


# Описание:

В проекте есть сборка (SubsetMath) с не реализованными методами, основной из них: SubSetFinder. GetSubset – этот метод ищет в коллекции элементы, сумма которых равна заданному значению, если таких элементов не получилось найти, то возвращается коллекция элементов, сумма которых наиболее близка к заданному значению, если ничего не получилось найти, то возвращается пустая коллекция.

В тестовой сборке есть файл (ValuesGroups.json) с примерами данных, которые можно использовать для тестирования, он состоит из отдельных  json объектов.
Для каждой группы в ValuesGroups.json можно найти такие подмножества, что бы условие удовлетворялось (тесты проходили). 
