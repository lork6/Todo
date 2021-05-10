import React, {useState} from 'react';
import './App.css';
import {DragDropContext, Droppable, Draggable} from "react-beautiful-dnd";
import _ from "lodash";
import {v4} from "uuid";

const item = {
  id: v4(),
  name: "Clean the house"
}

const item2 = {
  id: v4(),
  name: "Wash the car"
}

function App() {
  const [text, setText] = useState("")
  const [state, setState] = useState({
    "todo": {
      title: "Todo",
      items: [item, item2]
    },
    "in-progress": {
      title: "In Progress",
      items: []
    },
    "done": {
      title: "Completed",
      items: []
    }
  })

  const handleDragEnd = ({destination, source}) => {
    console.log(state);
    if (!destination) {
      return
    }

    if (destination.index === source.index && destination.droppableId === source.droppableId) {
      return
    }

    // Creating a copy of item before removing it from state
    const itemCopy = {...state[source.droppableId].items[source.index]}

    setState(prev => {
      prev = {...prev}
      // Remove from previous items array
      prev[source.droppableId].items.splice(source.index, 1)


      // Adding to new items array location
      prev[destination.droppableId].items.splice(destination.index, 0, itemCopy)

      return prev
    })
  }

  const addItem = () => {
    
    setState(prev => {
      return {
        
        ...prev,
        
        todo: {
          title: "Todo",
          items: [
            ...prev.todo.items,
            {
              id: v4(),
              name: text
            }
            
          ]
        }
      }
    })

    setText("")
  }

  const removeItem = (id,index) => {
    console.log(id,index);
    setState(prev => {
      prev = {...prev}
      prev[id].items.splice(index,1)
      return prev
    })
  }

  return (
    <div className="App">
      <div className="add" >
        <h3>Add item</h3>
        <input type="text" value={text} onChange={(e) => setText(e.target.value) }/>
        <button onClick={addItem} className="add-btn">Add</button>
      </div>
      <DragDropContext onDragEnd={handleDragEnd}>
        {_.map(state, (data, key) => {
          return(
            <div key={key} className={"column"}>
              <h1 className="title" >{data.title}</h1>
              <Droppable droppableId={key}>
                {(provided, snapshot) => {
                  return(
                    <div
                      ref={provided.innerRef}
                      {...provided.droppableProps}
                      className={"droppable-col "+key}
                    >
                      {data.items.map((el, index) => {
                        return(
                          <Draggable key={el.id} index={index} draggableId={el.id}>
                            {(provided, snapshot) => {
                              
                              return(
                                <div
                                  className={`item ${snapshot.isDragging && "dragging"}`}
                                  ref={provided.innerRef}
                                  {...provided.draggableProps}
                                  {...provided.dragHandleProps}
                                >
                                  <td className="item-text">{el.name}</td>
                                  <button className="Delete-btn" onClick={() => removeItem(key,index)}>
                                  Remove
                                  </button>
                                </div>
                              )
                            }}
                          </Draggable>
                        )
                      })}
                      {provided.placeholder}
                    </div>
                  )
                }}
              </Droppable>
            </div>
          )
        })}
      </DragDropContext>
    </div>
  );
}

<button className="add-btn" onClick={() => this.setPopup(true)}>Add</button>
        <Popup add={this.addItem} trigger={this.openPopup}  ></Popup>
export default App;