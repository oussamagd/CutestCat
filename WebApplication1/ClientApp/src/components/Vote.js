import React, { Component } from 'react';

export class Vote extends Component {
    displayName = Vote.name

    constructor(props) {
        super(props);
        this.state = { candidates: [], loading: true };
        fetch('https://localhost:44361/api/cat/Vote/Candidates')
            .then(response => response.json())
            .then(data => {
                this.setState({ candidates: data, loading: false });
            });
    }

    static voteCat = (e) => {

        let candidates = JSON.parse(e.target.alt)
        let winnerCat = candidates.find(c => c.url == e.target.src)
        let loserCat = candidates.find(c => c.url != e.target.src)

        fetch('https://localhost:44361/api/cat/Vote', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                WinnerCat: { 'reference': winnerCat.reference, 'url': winnerCat.url },
                loserCat: { 'reference': loserCat.reference, 'url': loserCat.url }
            })
        }).then(data => {
            if (data.status == 204) {
                window.location.reload();
            }
            else {
                console.log('une erreur est apparu, (il faut prévoir du temps pour gerer ces erreur)');
            }
        });
    }

    static renderCandidates(candidates) {
        return (<div className="row" >
            <div className="col-sm-5" >
                <img onClick={this.voteCat}
                    alt={JSON.stringify(candidates)}
                    src={candidates[0].url}
                    style={{ width: '300px', height: '300px' }} />
            </div>
            <div className="col-sm-2"></div>
            <div className="col-sm-5">
                <img onClick={this.voteCat}
                    alt={JSON.stringify(candidates)}
                    src={candidates[1].url}
                    style={{ width: '300px', height: '300px' }}></img>
            </div>
        </div>);
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Vote.renderCandidates(this.state.candidates);


        return (
            <div>
                <h3>cliquer sur l'image du chat le plus mignon afin de voter pour lui</h3>
                {contents}
            </div>
        );
    }
}
