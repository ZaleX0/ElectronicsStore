import React, { useEffect, useState } from 'react'
import { Button, FormControl, FormSelect, InputGroup, Pagination, Table } from 'react-bootstrap';
import { CheckLg } from 'react-bootstrap-icons';
import { useNavigate } from 'react-router-dom';
import OrderService from '../services/OrderService';
import { formatCurrency } from '../utilities/formatCurrency';
import { formatDate } from '../utilities/formatDate';

export default function OrdersAdmin() {
  const orderService = new OrderService();
  const navigate = useNavigate();
  const [orderPage, setOrderPage] = useState([]);
  const [orderItems, setOrderItems] = useState([]);

  const [showNotAccepted, setShowNotAccepted] = useState(false);
  const [searchPhrase, setSearchPhrase] = useState("");
  const [searchPhraseTmp, setSearchPhraseTmp] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(10);

  useEffect(() => {
    const fetchData = async () => {
      const response = await orderService.getAllOrders(showNotAccepted, searchPhrase, currentPage, itemsPerPage);
      if (response.status === 200) {
        setOrderPage(JSON.parse(response.text));
        setOrderItems(JSON.parse(response.text).items);
      } else {
        navigate("/orders");
      }
    }
    fetchData();
  }, [currentPage, itemsPerPage, searchPhrase, showNotAccepted])
  
  return (
    <>
      <h1 className="display-4 mb-2">Orders</h1>
      <div className="mb-2">{renderFilters()}</div>
      <Table variant="bordered" className="mb-2">
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
            <td>{formatDate(item.timeOrdered)}</td>
            <td>{formatDate(item.timeAccepted)}</td>
            <td>
              {item.products.map(p =>
                <div key={p.id}>
                  <span className="text-muted">x{p.quantity}</span> - {p.name}
                  <br/>
                </div>
              )}
            </td>
            <td>{formatCurrency(item.totalPrice)}</td>
            <td className="text-center align-middle">
              {renderAcceptButton(item)}
            </td>
          </tr>
        )}
        </tbody>
      </Table>
      {renderPagination()}
    </>
  )

  function renderAcceptButton(item) {
    const acceptOrder = async (event) => {
      event.target.disabled = true;
      await orderService.acceptOrder(event.target.id);
    }
    return item.isAccepted
      ? <CheckLg size={50} color="green" />
      : <Button variant="success" id={item.id} onClick={acceptOrder}>
          Accept
        </Button>
  }

  function renderFilters() {
    const handleShowNotAcceptedChange = () => {
      setShowNotAccepted(!showNotAccepted);
      setCurrentPage(1);
    }
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
      <div className="d-flex justify-content-end">
        <div>
          <InputGroup>
            <InputGroup.Checkbox checked={showNotAccepted} onChange={handleShowNotAcceptedChange}/>
            <InputGroup.Text>Show not accepted</InputGroup.Text>
          </InputGroup>
        </div>  
        <form className="ms-2" onSubmit={handleSearchClick}>
          <InputGroup>
            <FormControl placeholder="Search user email" value={searchPhraseTmp} onChange={handleSearchChange}/>
            <Button type="submit">Search</Button>
            <Button variant="outline-primary" onClick={handleSearchClear}>Clear</Button>
          </InputGroup>
        </form> 
      </div>
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
            <option value={20}>20</option>
            <option value={30}>30</option>
          </FormSelect>
        </InputGroup>
      </div>
    )
  }
}