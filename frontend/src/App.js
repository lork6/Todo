import React, {useState,useEffect } from 'react';
import './App.css';
import {DragDropContext, Droppable, Draggable,DroppableStateSnapshot} from "react-beautiful-dnd";
import _ from "lodash";
import axios from 'axios';
import Popup from './components/popup';


const api = axios.create({
  baseURL: "http://localhost:62151/api/TodoItems/"
})

class App extends React.Component{
  state = {
    "todo": {
      title: "Todo",
      items: [],
      index: 0,
      render: this.GetTodo
    },
    "in-progress": {
      title: "In Progress",
      items: [],
      index: 1,
      render: this.GetProgress
    },
    "done": {
      title: "Completed",
      items: [],
      index: 2,
      render: this.GetDone
    },
    "postponed":{
      title:"Postponed",
      items:[],
      index: 3,
      render: this.GetPostponed
    }
  };
  
  draggedItem = null;

  constructor(props) {
    super(props);
    
      this.isLoaded0= false;
      this.isLoaded1=  false;
      this.isLoaded2=  false;
      this.isLoaded3= false;
      this.items= [];
      this.error= null;
    
    
    this.text = "";
    this.refresh(); 
    
   
  
  }
  componentDidMount(){
   this.refresh(); 
  }
  
  shouldComponentUpdate(nextProps,next) {
    if (this.state === next) {
      return false;
    }
    return true;
  }
  refresh = async () => {
    
    await this.GetTodo();
    await this.GetPostponed();
    await this.GetProgress();
    await this.GetDone();
    console.log(this.state.todo.items)
    console.log(this.state["in-progress"].items)
    console.log(this.state.done.items)
    console.log(this.state.postponed.items)
  }
         GetTodo = async () => {
            let data =  await api.get("/todo")
                .then(({data}) => data)
            this.setState(prev =>{
              return {
                ...prev,
                todo: {
                  index: 0,
                  render:this.GetTodo,
                  title: "Todo",
                  items: data,
                  
                }
              }

            })
          }
          GetProgress= async () => {
            let data =  await api.get("/in-progress")
                .then(({data}) => data)
            this.setState(prev =>{
              return {
                ...prev,
                ["in-progress"]: {
                  
                  index: 1,
                  render:this.GetProgress,
                  title: "In Progress",
                  items: data,
                  
                }
              }

            })
          }
          GetDone = async()=>{
            let data =  await api.get("/done")
                .then(({data}) => data)
            this.setState(prev =>{
              return {
                ...prev,
                done: {
                  index: 2,
                  render:this.GetDone,
                  title: "Complete",
                  items: data,
                  
                }
              }

            })
          }
          GetPostponed =async ()=> {
            let data =  await api.get("/postponed")
                .then(({data}) => data)
            this.setState(prev =>{
              return {
                ...prev,
                postponed: {
                  index: 3,
                  render:this.GetPostponed,
                  title: "Postponed",
                  items: data,
                  
                }
              }

            })
          }
    onDragStart =  async (results) => {
        console.log("drag start "+this.state[results.source.droppableId].items[results.source.index].id)
        this.draggedItem = this.state[results.source.droppableId].items[results.source.index].id;
        
    }
    onDragUpdate = async ({destination, source}) => {
            if (!destination) {
              return
            }
            if (destination.index === source.index && destination.droppableId === source.droppableId) {
              return
            }
            
            console.log("fasd "+this.draggedItem)
            //console.log("wtf "+source.draggableId)
            //this.draggedItem = {...this.state[droppableId].items[source.index]};
            let item = await api.get("/"+this.draggedItem).then(({data})=>data);
            console.log(item);
            let data = await api.put("/"+item.id,
            { id:Number( item.id),
            name: item.name,
            date: item.date,
            description:item.description,
            complete: Number(this.state[destination.droppableId].index),
            order:Number(destination.index)
      }
    ).catch(err=>{console.log(err)}).then(()=>{
      
    })
    await this.refresh();
    this.forceUpdate();
          }
    
  
  handleDragEnd = async ({destination, source}) => {
    if (!destination) {
      return
    }

    if (destination.index === source.index && destination.droppableId === source.droppableId) {
      return
    }
    
    const itemCopy = await api.get("/"+this.draggedItem).then(({data})=>data);

    console.log("move item"+" "+itemCopy.date)
    let data = await api.put("/"+itemCopy.id,
    {id:Number( itemCopy.id),
      name: itemCopy.name,
      date:itemCopy.date,
      description:itemCopy.description,
      complete: Number(this.state[destination.droppableId].index),
      order:Number(destination.index)
      }
    ).catch(err=>{console.log(err)}).then(()=>{
      this.refresh()})
      
      await this.refresh();
      this.forceUpdate();
    
    
   
  }
  
  addItem = async (title,desc,date) => {
    console.log("additem"+" "+date)
    let data = await api.post("/",{
      name:title,
      date:date,
      description:desc,
      order: this.state.todo.items.length,
      complete:0
    })
    await this.refresh()
  }
  
  removeItem (id)  {
    fetch("http://localhost:62151/api/TodoItems/"+id,{
      method:"DELETE"
    }).then(res => res.json()).then().catch().finally(()=>{this.refresh()})
    console.log("delete id: "+id)
    
    
  }
  
  render(){
    console.log(this.state+" render" + this.isLoaded0 && this.isLoaded1 && this.isLoaded2 &this.isLoaded3)
    /*if(!this.isLoaded0 || !this.isLoaded1 || !this.isLoaded2 || !this.isLoaded3){
      return(
        <div className="App">Loding...</div> 
      );
    }*/
    
    if(true){
      console.log(this.state.todo.items+" render 2")
    return (
      
      <div className="App">
        
        <MyButton add={this.addItem}></MyButton>
        <DragDropContext onDragEnd={this.handleDragEnd} onDragUpdate={this.onDragUpdate} onDragStart={this.onDragStart}>
          {_.map(this.state, (data, key) => {
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
                            <Draggable key={el.id.toString()} index={index} draggableId={el.id.toString()}>
                              {(provided, snapshot) => {
                                
                                return(
                                  <div
                                    className={`item ${snapshot.isDragging && "dragging"}`}
                                    ref={provided.innerRef}
                                    
                                    {...provided.draggableProps}
                                    {...provided.dragHandleProps}
                                  >
                                    <div className="item-text">{el.name}</div>
                                    <div className="item-text">{el.description}</div>
                                    <div>{new Date(el.date).getFullYear()}/{new Date(el.date).getMonth()}/{new Date(el.date).getDate()}</div>
                                    <button className="Delete-btn" onClick={() => this.removeItem(el.id)}>
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
}}}

function MyButton(props){
  const [openPopup,setopenPopup] = useState(false);

  return (
    <div>
  <button className="add-btn" onClick={() => setopenPopup(true)}>Add</button>
  <Popup add={props.add} trigger={openPopup} setTrigger={setopenPopup} ></Popup></div>)
}

export default App;