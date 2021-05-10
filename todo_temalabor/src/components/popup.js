import React,  {useState} from 'react';
import "./popup.css";
import DatePicker from 'react-datepicker/dist/react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';

function Popup(props){
    const [title, setText] = useState("")
    const [description, setDescription] = useState("")
    const [selectDate, setDate] = useState(null)
    console.log("inside popup "+props.trigger)
    return (props.trigger) ? (
        <div className="popup">
            <div className="popup-inner" >
                <h3>Add item</h3>
                <div>
                    <label for="name">Name</label>
                    <input id="name" type="text" value={title} onChange={(e) => setText(e.target.value) }/>
                </div>
                <div>
                    <label for="description">Description</label>
                    <input id="description" type="text" value={description} onChange={(e) => setDescription(e.target.value) }/>
                </div>
                <div>
                    <label for="date">Date</label>
                    <DatePicker selected={selectDate} onChange={date => setDate(date)} dateFormat='yyyy/MM/dd' minDate={new Date()}/>
                </div>

                <button onClick={() => props.add(title,description,selectDate)} className="add-btn">Add</button>
                <button onClick={() => props.setTrigger(false)}>close</button>
                
            </div>

        </div>
    ) : "";
}

export default Popup;