import React,  {useState} from 'react';
import "./popup.css";
import DatePicker from 'react-datepicker/dist/react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import {Form,Button} from 'react-bootstrap';

function Popup(props){
    const [title, setText] = useState("")
    const [description, setDescription] = useState("")
    const [selectDate, setDate] = useState(null)
    console.log("inside popup "+props.trigger)
    return (props.trigger) ? (
        
        <div className="popup">
            <Form>
                <div className="popup-inner" >
                    <Form.Text>
                        <h3>Add item</h3>
                    </Form.Text>
                    <Form.Group>
                        <Form.Label htmlFor="name">Name</Form.Label>
                        <Form.Control required maxlength="15" id="name" type="text" value={title} onChange={(e) => setText(e.target.value) }/>
                    </Form.Group>
                    <Form.Group>
                        <Form.Label htmlFor="description">Description</Form.Label>
                        <Form.Control id="description" maxlength="50" type="text" value={description} onChange={(e) => setDescription(e.target.value) }/>
                    </Form.Group>
                    <Form.Group>
                        <Form.Label htmlFor="date">Date</Form.Label>
                        <br />
                        <DatePicker required className="form-control" selected={selectDate} onChange={date => setDate(date)} dateFormat='yyyy/MM/dd' minDate={new Date()}/>
                    </Form.Group>

                    <Button onClick={() => props.add(title,description,selectDate)} className="add-btn">Add</Button>
                    <Button className="btn-secondary" onClick={() => props.setTrigger(false)}>close</Button>
                
                </div>
            </Form>
        </div>
    ) : "";
}

export default Popup;