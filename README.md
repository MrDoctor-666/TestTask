# TestTask
Ужасно, ужасно бюрократическая компания

Не уверена, можно ли публиковать условие, поэтому кто знает, тот знает.

Для работы сначала создается Компания (*Company*) с числом отделов и печатей. 

*CreateDepartment* создает отдел (их для работы нужно столько же, сколько указано при создании комании).
Первый аргумент - тип (0 - безусловный, 1 - условный), далее в соответствии с заданием i, j, k, s, t, r, p (последние 4 можно не добавлять, ели тип безусловный).

*StartDetour* начинает обход Васи. Аргументы - начало и конец пути. Если возвращается true, то Вася потерялся в бесконечном цикле.

*WasVisited* возвращает, был ли посещен отдел, в аргументах передается номер отдела и список, в который требуется поместить информацию об обходном листе (две перегрузки, если нужны все списки или только последний).
