# Todo application

- [Requirements](#requirements)

- [Install](#install)

- [Documents](#documents)
  
  

## Requirements

To run the frontend you need  node.js and yarn.

For [Node.js](https://nodejs.org/en/) download and install.

After Node.js is intalled you can install yarn by

`npm install --global yarn`

## Install

First colne or download the project. After that you want to go to */frontend* folder, in the folder you want to run this command in cmd.

`npm install`

This will install all the

### Starting the app

After the install is finished you want to run this command in  

*/frontend* folder

`npm start`

to start running the frontend app.

## Documents

- [Backend](#backend)

- [Frontend](#frontend)

## Backend

- [Rest api](#rest api)

- [Models](#models)

### Rest api

Backend url: `http://localhost:62151/`

- [GET](#Http GET request:)

- PUT

- DELETE

- POST

#### Http GET request:

- Get all Items: `api/TodoItems/`

- Get item by id: `api/TodoItems/{id}` 

- Get all items from todo column: `api/TodoItems/todo`

- Get all items from In Progress column: `api/TodoItems/in-progress`

- Get all items from Completed column: `api/TodoItems/done`

- Get all items from todo column: `api/TodoItems/postponed`

All will return with json.

#### Http PUT request:

- `api/TodoItems/{id}` in body json with [Todo item](#todo item). 

##### Changeing column

For Changeing `Todo item` column. You have to specifi wich column you want to put it in by changeing `complete` variable with column index. Column Index is start at 0 and sending in to the server.

##### Changeing order in column

For Changeing `Todo item` order in column. You have to specifi wich position you want the`Todo item` by changeing the `order` variable and sending in to the server. Indexing is start at 0 end is length of the column.

#### Http DELETE request:

- `api/TodoItems/{id}`



#### Http POST request:

- `api/TodoItems/ in body json with [Todo item](#todo item).

### Models

#### Todo item

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



## Frontend


