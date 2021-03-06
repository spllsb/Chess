const app = document.getElementById('root')


const container = document.createElement('div')
container.setAttribute('class', 'container')

app.appendChild(container)

var request = new XMLHttpRequest()
request.open('GET', 'http://localhost:5000/api/users/user1@email.com', true)
request.onload = function() {
  // Begin accessing JSON data here
    var user = JSON.parse(this.response)
    if (request.status >= 200 && request.status < 400) {

        const card = document.createElement('div')
        card.setAttribute('class', 'card')

        const h1 = document.createElement('h1')
        h1.textContent = user.email

        const p = document.createElement('p')
        p.textContent = user.username

        container.appendChild(card)
        card.appendChild(h1)
        card.appendChild(p)
    
    } else {
        const errorMessage = document.createElement('marquee')
        errorMessage.textContent = `Gah, it's not working!`
        app.appendChild(errorMessage)
    }
}

request.send()