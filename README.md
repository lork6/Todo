# Todo application

- [Features](#features)

- [Requirements](#requirements)

- [Install](#install)

- [Documents](#documents)

- [Know issues](#know-issues)

## Features

An web app wich you can add cards. 

Cards can be move around in columns.

Cards can be removed.

Cards can store title, description and date.

Columns represents the cards status.

Cards change colors by which column they are in.

## Requirements

### Frontend requirements

To run the frontend you need  node.js and yarn.

For [Node.js](https://nodejs.org/en/) download and install.

After Node.js is intalled you can install yarn by running this command in cmd.

`npm install --global yarn` 

### Backend requirements

To run backend you need .NET 5.0 version

## Install

- [Frontend](#install-frontend)

- [Backend](#install-backend)

### Install frontend

First colne or download the project. After that you want to go to */frontend* folder, in the folder you want to run this command in cmd.

`npm install`

This will install all the packages for frontend.

#### Starting the app

After the install is finished you want to run this command in

*/frontend* folder in cmd

`npm start`

to start running the frontend app.

### Install Backend



## Documents

- [Backend](#backend)

- [Frontend](#frontend)

## Backend

- [Rest api](#rest-api)

- [Models](#models)

- [Data](#data)

- [Controllers](#controllers)

- [Startup cs](#startup-cs)

### Rest api

Backend base url: `http://localhost:62151/`

- [GET](#http-get-request)

- [PUT](#http-put-request)

- [DELETE](#http-delete-request)

- [POST](#http-post-request)

#### Http GET request:

- Get all Items: `api/TodoItems/`

- Get item by id: `api/TodoItems/{id}` 

- Get all items from todo column: `api/TodoItems/todo`

- Get all items from In Progress column: `api/TodoItems/in-progress`

- Get all items from Completed column: `api/TodoItems/done`

- Get all items from todo column: `api/TodoItems/postponed`

All will return with json.

#### Http PUT request:

- `api/TodoItems/{id}` in body json with [Todo item](#todo-item). 

##### Changeing column

For Changeing `Todo item` column. You have to specifi wich column you want to put it in by changeing `complete` variable with column index and sending in to the server. Column Index is start at 0.

##### Changeing order in column

For Changeing `Todo item` order in column. You have to specifi wich position you want the`Todo item` by changeing the `order` variable and sending in to the server. Indexing is start at 0 end is length of the column.

#### Http DELETE request:

- `api/TodoItems/{id}`



#### Http POST request:

- `api/TodoItems/` in body json with [Todo item](#todo-item).

### Models

#### Todo item

Entity

```
 public class TodoItem
    {
        public long Id { get; set; }
        public string name { get; set; }
        public int complete { get; set; }
        public int order { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
    }
```

### Data

#### SqlTodoRepository

`SqlTodoRepository` Class is responsible for getting the data form Database and returning to Controller.

`UpdateOrders` function is wich sets the [Todo items](#todo-item) order if one [Todo item](#todo-item) position changed.

### Controllers

#### TodoItemsController

`TodoItemsController` class is responsible for manageing [Rest api](#rest-api).

### Startup cs

If you want to change Database connection then you have to change this.

```
services.AddDbContext(opt =>
 opt.UseInMemoryDatabase("TodoList"));
```



## Frontend

### Packages

- [react](https://reactjs.org/)

- [Lodash](https://lodash.com/)

- [react-beautiful-dnd](https://github.com/atlassian/react-beautiful-dnd)

- [axios](https://github.com/axios/axios)

### App js



### Popup js



## Know issues

1. If you fast grab a card and place it to another column, then the card is can't be grabbed again until a another card is grabbed.
   
   This is beacuse the change is made in while grabbing (onDragUpdate) the card. onDragUpdate function is sending the new [Todo item](#todo-item) to the server.  if you do it fast the [react-beautiful-dnd](https://github.com/atlassian/react-beautiful-dnd) will not update properly.
   
   One solution is doing it in onDragEnd, but then the card is flickering for a second between two position.










