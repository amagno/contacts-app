import './index.css'
import axios from 'axios'


const apiUrl = 'https://localhost:5001/api/contacts/'
const form = document.getElementById("CreateContactForm")

form.onsubmit = async event => {
  event.preventDefault()

  const formData = new FormData(event.target)

  const object = {};
  formData.forEach(function(value, key){
      object[key] = value;
  });

  const response = await axios.post(apiUrl, object)

  console.log(response);

}