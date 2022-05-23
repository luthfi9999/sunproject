import React from "react";
import axios from "axios";

const Users = [
  {
    id: 1,
    selected: false,
    store: "111",
    store_Name: "Toko Jakarta"
  },
  {
    id: 2,
    selected: false,
    storesId: "222",
    Name: "Toko Malang"
  },
  {
    id: 3,
    selected: false,
    storesId: "333",
    Name: "Toko Surabaya"
  },
];

class SelectTableComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      List: [{id:'', store: '', store_Name:'', selected:false}],
      MasterChecked: false,
      SelectedList: [],
    };
    this.onItemCheck = this.onItemCheck.bind(this);
    this.onMasterCheck = this.onMasterCheck.bind(this);
  }


  // Select/ UnSelect Table rows
  onMasterCheck(e) {

    let tempList = this.state.List;
    // Check/ UnCheck All Items
    tempList.map((user) => (user.selected = e.target.checked));

    //Update State
    this.setState({
      MasterChecked: e.target.checked,
      List: {tempList},
      SelectedList: this.state.List.filter((e) => e.selected),
    });
  }

  componentDidMount()
  {
    console.log('test');
    axios
    .get("http://localhost:5281/")
    .then( res => {
      console.log(res.data)
      this.setState({
        MasterChecked: false,
        List: res.data.storeList,
        SelectedList: [],
      });
    })
    .catch( err => console.log(err))
  }

  componentDidUpdate() {
    // Typical usage (don't forget to compare props):
    this.props.onSelectedChange(this.state.SelectedList);
  }

  // Update List Item's state and Master Checkbox State
  onItemCheck(e, item) {
    let tempList = this.state.List;
    tempList.map((user) => {
      if (user.store === item.store) {
        user.selected = e.target.checked;
      }
      return user;
    });

    //To Control Master Checkbox State
    const totalItems = this.state.List.length;
    const totalCheckedItems = tempList.filter((e) => e.selected).length;

    // Update State
    this.setState({
      MasterChecked: totalItems === totalCheckedItems,
      List: tempList,
      SelectedList: this.state.List.filter((e) => e.selected),
    });

  }

  render() {
    return (
      <div className="container scroll">
        <div className="row">
          <div className="col-md-12">
            <table className="table">
              <thead>
                <tr>
                  <th scope="col">
                    <input
                      type="checkbox"
                      className="form-check-input"
                      checked={this.state.MasterChecked}
                      id="mastercheck"
                      onChange={(e) => this.onMasterCheck(e)}
                    />
                  </th>
                  <th scope="col">storesId</th>
                  <th scope="col">Name</th>
                </tr>
              </thead>
              <tbody>
                {this.state.List.length >0 && this.state.List.map((user) => (
                  <tr key={user.store} className={user.selected ? "selected" : ""}>
                    <th scope="row">
                      <input
                        type="checkbox"
                        checked={user.selected}
                        className="form-check-input"
                        id="rowcheck{user.store}"
                        onChange={(e) => this.onItemCheck(e, user)}
                      />
                    </th>
                    <td>{user.store}</td>
                    <td>{user.store_Name}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    );
  }
}

export default SelectTableComponent;