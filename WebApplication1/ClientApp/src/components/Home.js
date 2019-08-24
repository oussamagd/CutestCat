import React, { Component } from 'react';

export class Home extends Component {
    displayName = Home.name

    render() {
        return (
            <div>
                <h1>Salut!</h1>
                <p>CutestCat est une mini application web qui permet de trouver le chat le plus mignon.</p>
                <p>Pour  voir les chats avec leur score cliquer sur chats</p>
                <p>Pour pouvoir voter clicker sur vote</p>
            </div>
        );
    }
}
