import './index.css'
import axios from 'axios'


const apiUrl = 'https://localhost:5001/api/contacts/'
const form = document.getElementById("CreateContactForm")
const container = document.getElementById('CreateContactFormContainer')
const button = document.getElementById('ToggleCreateContactForm')

let showCreateContactForm = false;

container.style.display = 'none';


const toggleCreateContactForm = () => {
  showCreateContactForm = !showCreateContactForm;

  container.style.display = showCreateContactForm ? 'block' : 'none'
  button.innerText = !showCreateContactForm ? 'Create Contact' : 'Hide'
}

button.onclick = event => {
  event.preventDefault()
  toggleCreateContactForm()
}

form.onsubmit = async event => {
  event.preventDefault()

  const formData = new FormData(event.target)

  const object = {};
  formData.forEach((value, key) => {
      object[key] = value;
  });

  const response = await axios.post(apiUrl, object)

  if (response.status !== 201)
  {
    alert('Error on create contact')
  }

  renderContacts()
  toggleCreateContactForm()
}

const createContactHtml = contact => `
<div class="contact-card">
  <div class="contact-card-field">
    ${contact.firstName} ${contact.lastName}
  </div>
  <div class="contact-card-field">
    ${contact.email}
  </div>
  <div class="contact-card-field">
    ${contact.birthday}
  </div>
  <div class="contact-card-field">
  </div>
  <div class="contact-card-info">
    <div class="contact-card-field">
    </div>
    <div class="contact-card-field">
    </div>
    <div class="contact-card-field">
    </div>
    <div class="contact-card-field">
    </div>

  </div>
</div>
`.trim()


const renderContacts = async () => {
  const response = await axios.get(apiUrl)
  const contacts = response.data
  let html = ''
  contacts.forEach(contact => {
    html += createContactHtml(contact)
  })

  document.getElementById('ContactsList').innerHTML = html
}

renderContacts()