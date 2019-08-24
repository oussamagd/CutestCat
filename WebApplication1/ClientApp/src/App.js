import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Cats } from './components/Cats';
import { Vote } from './components/Vote';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
            <Route path='/Vote' component={Vote} />
        <Route path='/Cats' component={Cats} />
      </Layout>
    );
  }
}
