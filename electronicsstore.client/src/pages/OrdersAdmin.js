import React, { useEffect, useState } from 'react'
import { Button, FormControl, FormSelect, InputGroup, Pagination, Table } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import OrderService from '../services/OrderService';

export default function OrdersAdmin() {
  const orderService = new OrderService();
  const navigate = useNavigate();
  const [orderPage, setOrderPage] = useState([]);
  const [orderItems, setOrderItems] = useState([]);

  const [searchPhrase, setSearchPhrase] = useState("");
  const [searchPhraseTmp, setSearchPhraseTmp] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(10);

  useEffect(() => {
    const fetchData = async () => {
      const response = await orderService.getAllOrders(searchPhrase, currentPage, itemsPerPage);
      if (response.status === 200) {
        setOrderPage(JSON.parse(response.text));
        setOrderItems(JSON.parse(response.text).items);
      } else {
        navigate("/orders");
      }
    }
    fetchData();
  }, [currentPage, itemsPerPage, searchPhrase])
  
  return (
    <>
      <h1 className="display-4 mb-2">My orders</h1>
      <div className="d-flex justify-content-end mb-2">{renderSearchUser()}</div>
      <Table variant="bordered">
        <thead className="table-primary">
          <tr>
            <th>Id</th>
            <th>User</th>
            <th>Time ordered</th>
            <th>Time accepted</th>
            <th>Products</th>
            <th>Total price</th>
            <th>Accepted</th>
          </tr>
        </thead>
        <tbody>
        {orderItems.map(item =>
          <tr key={item.id}>
            <td>{item.id}</td>
            <td>{item.userEmail}</td>
            <td>{item.timeOrdered}</td>
            <td>{item.timeAccepted}</td>
            <td>
              {item.products.map(p =>
                <>
                  {p.name}
                  <br/>
                </>
              )}
            </td>
            <td>{item.totalPrice}</td>
            <td>{item.isAccepted ? "true" : "false"}</td>
          </tr>
        )}
        </tbody>
      </Table>
      {renderPagination()}
    </>
  )

  function renderSearchUser() {
    const handleSearchChange = (event) => {
      setSearchPhraseTmp(event.target.value);
    }
    const handleSearchClick = (event) => {
      event.preventDefault();
      setSearchPhrase(searchPhraseTmp);
      setCurrentPage(1);
    }
    const handleSearchClear = () => {
      setSearchPhraseTmp("");
      setSearchPhrase("");
      setCurrentPage(1);
    }

    return (
      <form onSubmit={handleSearchClick}>
        <InputGroup>
          <FormControl placeholder="Search user email" value={searchPhraseTmp} onChange={handleSearchChange}/>
          <Button type="submit">Search</Button>
          <Button variant="outline-primary" onClick={handleSearchClear}>Clear</Button>
        </InputGroup>
      </form>
    )
  }

  function renderPagination() {
    const handleItemsPerPageChange = (event) => {
      setItemsPerPage(event.target.value);
      setCurrentPage(1);
    }

    let paginationItems = [];
    for (let i = 1; i <= orderPage.totalPages; i++) {
      paginationItems.push(
        <Pagination.Item key={i} active={i === currentPage} onClick={() => setCurrentPage(i)}>
          {i}
        </Pagination.Item>
      );
    }
    return (
      <div className="d-flex justify-content-between">
        <Pagination className="m-0">
          {paginationItems}
        </Pagination>
        <InputGroup className="ms-2 w-auto">
          <InputGroup.Text>Orders per page</InputGroup.Text>
          <FormSelect value={itemsPerPage} onChange={handleItemsPerPageChange}>
            <option value={2}>2</option>
            <option value={5}>5</option>
            <option value={10}>10</option>
            <option value={15}>15</option>
          </FormSelect>
        </InputGroup>
      </div>
    )
  }
}