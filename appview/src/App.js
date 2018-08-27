import React, { Component } from 'react';
import axios from 'axios'
import ReactDOM from 'react-dom';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import './App.css';
import '../node_modules/react-bootstrap-table/dist/react-bootstrap-table-all.min.css';
import 'bootstrap/dist/css/bootstrap.min.css';

class App extends Component {

  constructor(props) {
    super(props);
    this.state = {
      heightSearch: '',
      widthSearch: '',
      typePrice:'',
      displays: [],
      totalPrice: 0.00
    };

    this.handleChange = this.handleChange.bind(this);
    this.setTypePrice = this.setTypePrice.bind(this);
    this.calculatePriceTotal = this.calculatePriceTotal.bind(this);
  }

  

  setTypePrice(event) {
    this.setState({ value: event.target.value });
    this.state.typePrice = event.target.value;

    updateList(this);
  }

  calculatePriceTotal(event) {
    var sum = 0.00;
    for (var i=0; i < this.state.displays.length; i++){
      sum += this.state.displays[i].price;
    }

    ReactDOM.render(`Total: $ ${(sum).toFixed(2)}`, document.getElementById('priceTotal'));
  }
  
  handleChange(event) {
    this.setState({[event.target.name]: event.target.value} );
    if(event.target.name == 'heightSearch'){
      this.state.heightSearch = event.target.value;
    }else{
      this.state.widthSearch = event.target.value;
    }
    
    updateList(this);
  }

  render() {
    return (
    <div>
      <div id="top" class="row">
        <div class="col-sm-3">
          <h2>Display Search</h2>
        </div>
      </div>
      <p/>

      <div id="top" class="row">
        <div class="col-sm-1">
          <div class="h2">
            <input name="heightSearch" class="form-control" id="heightSearch" type="text" placeholder="Height" value={this.state.heightSearch} onChange={this.handleChange}/>
          </div>
        </div>

        <span aria-hidden='true' class="h2">:</span>

        <div class="col-sm-1">
          <div class="h2">
            <input name="widthSearch" class="form-control" id="widthSearch" type="text" placeholder="Width" value={this.state.widthSearch} onChange={this.handleChange}/>
          </div>
        </div>

        <div class="col-sm-2">
          <select class="custom-select d-block w-100" id="typePrice" required="" value={this.state.typePrice} onChange={this.setTypePrice}>
              <option value="">Type Price ...</option>
              <option value="0">Cheap</option>
              <option value="1">Normal</option>
            </select>
        </div>

        <div class="col-sm-1">
          <a class="btn btn-primary h2" onClick={this.calculatePriceTotal}> Calculate</a>
        </div>

        <div class="col-sm-3">
          <p><span aria-hidden='true' id="priceTotal" class="h2">Total: $ 0.00</span></p>
        </div>

      </div>
          
      <hr />

      <div id="table"></div>

    </div>
    );
  }

  
}

function sizeFormater(cell) {
  return `<p><span aria-hidden='true'></span> ${cell.height} X ${cell.width} px</p>`;
}

function decimalFormater(cell) {
  return `<p><span aria-hidden='true'></span> $ ${(cell).toFixed(2)}</p>` ;
}

function updateList(th) {
  const qs = require('qs');
  axios.post('http://localhost:17853/Display/Consult', qs.stringify({ height: th.state.heightSearch, width: th.state.widthSearch, typePrice: th.state.typePrice }))
       .then(function(response){
          th.state.displays = response.data;
            ReactDOM.render(
              <BootstrapTable data={ th.state.displays }>
                <TableHeaderColumn dataField='id' isKey={ true }>Product ID</TableHeaderColumn>
                <TableHeaderColumn dataField='displaySize' dataFormat={ sizeFormater }>Size</TableHeaderColumn>
                <TableHeaderColumn dataField='price' dataFormat={ decimalFormater }>Price</TableHeaderColumn>
              </BootstrapTable>,
              document.getElementById('table')
            );
      })
      .catch(function (error) {
        console.log(error);
      });  
}
export default App;
