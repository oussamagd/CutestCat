import React, { Component } from 'react';

export class Cats extends Component {
    displayName = Cats.name

    constructor(props) {
        super(props);
        this.state = { cats: [], loading: true };

        fetch('https://localhost:44361/api/cat')
            .then(response => response.json())
            .then(data => {
                this.setState({ cats: data, loading: false });
            });
    }

    static renderCats(cats) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>Reference</th>
                        <th>Image</th>
                        <th>Score</th>
                    </tr>
                </thead>
                <tbody>

                    {cats.map(cat =>
                        <tr key={cat.reference}>
                            <td>{cat.reference}</td>
                            <td><img src={cat.url} style={{ width: '150px', height: '150px' }} /></td>
                            <td>{cat.score}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Cats.renderCats(this.state.cats);

        return (
            <div>
                <h1>Nos chats avec leurs resultats</h1>
                {contents}
            </div>
        );
    }
}
