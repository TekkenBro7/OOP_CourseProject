### Снежко Максим Андреевич 

### группа 253505

# Программный инструмент

--------------------------------------------------
Программное обеспечение для планирования задач будет включать в себя основные функции, с помощью которых вы сможете вести организацию задач, управление временем и повышать производительность выполенения задач. Это программное обеспечение будет разработано с использованием языковых инструментов C# и платформы .NET версии 8.0. Для создания графического приложения также будет использоваться платформа .NET MAUI (NET Multi-Platform App UI).

# Функциональные требования

--------------------------------------------------

Это приложение предназначено для планирования своих дел на день или на будущие дни. Прежде всего пользователю необходимо войти в учетную запись или создать ее, после чего ему будет доступен весь функционал приложения: создание задач, редактирование, удаление, сортировка. Приложение будет иметь возможность добавлять подзадачи к существующим задачам, чтобы разделить их на более простые компоненты. Также можно будет комментировать задачи и проекты для пошагового описания реализации задач. Можно отслежаивать выполение задач (Пользователь может отмечать задачи как выполненные по мере их завершения, можно ставить статус выполнения, например, "Ожидает выполнения", "Выполняется", "Выполнено"). Система может отправлять уведомления пользователям о новых задачах, изменениях статуса задач и других событиях. Пользователи могут фильтровать задачи по различным критериям, таким как приоритет, статус и дата создания. C подзадачами можно делать то же самое, что и с задачами( добавление, редактирование, удаление комментариев, использование приоритетов, статусов).

Задачу можно относить к конкретной категории, например, "Личное", "Учеба", "Работа", расставлять по приоритету(в один день несколько различных задач). Можно комментировать задачу, добавлять комментарии для уточнений. К каждой задачи можно создавать подзадачи для удобного отслеживания и выполнения основной задачи. К каждой задаче, а также подзадаче можно устанавливать статус.

Класс AuthManager предназначен для создание пользователя, если его нет, вход в аккаунт, а также выход из него и смена аккаунта.

# Диаграмма класса проекта

--------------------------------------------------


![image](https://github.com/TekkenBro7/OOP_CourseProject/assets/114312277/76302079-8628-4bf8-bb5a-155f9296af3b)




# Компоненты проекта
--------------------------------------------------

- Файлы .cs реализуют классы и их функции для работы с задачами.
- Файлы .xaml реализуют графический интерфейс этого программного инструмента.
- Файлы .svg описывают изображения объектов графического интерфейса программного обеспечения в виде фигур, линий, текста и фильтров.
