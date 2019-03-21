/// <reference types="Cypress" />

const faker = require('faker')

context("Contacts App", () => {
  beforeEach(() => {
    cy.visit("/")
  })

  it("Show Title Text", () => {
    cy
      .get("#content")
      .children("h1")
      .first()
      .should("have.text", "Contacts APP")
  })

  it('Test create new contact', () => {
    cy.server()
    cy.route('POST', '**/api/contacts/').as('createContact')
    
    cy.get('input[name="FirstName"]').type(faker.name.firstName())
    cy.get('input[name="LastName"]').type(faker.name.lastName())
    cy.get('input[name="Email"]').type(faker.internet.email())
    cy.get('input[name="Birthday"]').type('1991-01-01')
    cy.get('input[name="Company"]').type(faker.company.companyName())
    cy.get('input[name="Address"]').type(faker.address.secondaryAddress())
    cy.get('input[name="Avatar"]').type(faker.internet.avatar())
    cy.get('input[name="Phone"]').type(faker.phone.phoneNumber("(##) #####-####"))

    cy.get('button[type="submit"]').click()

    cy.wait('@createContact').then(xhr => {
      assert.equal(201, xhr.status)
    })

  })
})