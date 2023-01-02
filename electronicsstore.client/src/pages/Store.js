import React, { useEffect, useState } from 'react'
import { Button, FormControl, FormSelect, InputGroup, Pagination } from 'react-bootstrap';
import { ArrowDown, ArrowUp } from 'react-bootstrap-icons';
import { StoreItem } from '../components/store/StoreItem'
import StoreService from '../services/StoreService'

export function Store() {
  const storeService = new StoreService();
  const [storePage, setStorePage] = useState({});
  const [storeItems, setStoreItems] = useState([]);
  const [categories, setCategories] = useState([]);

  const [sortBy, setSortBy] = useState("Name");
  const [sortDirection, setSortDirection] = useState(0);
  const [searchPhrase, setSearchPhrase] = useState("");
  const [searchPhraseTmp, setSearchPhraseTmp] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(5);
  const [currentCategoryId, setCurrentCategoryId] = useState("");

  const fetchData = async () => {
    const pagedProducts = await storeService.getProducts(searchPhrase, currentPage, itemsPerPage, sortBy, sortDirection, currentCategoryId);
    const categories = await storeService.getCategories();
    setCategories(categories);
    setStorePage(pagedProducts);
    setStoreItems(pagedProducts.items);
  }

  useEffect(() => {
    fetchData();
  }, [currentPage, itemsPerPage, currentCategoryId, sortBy, sortDirection, searchPhrase])

  return (
    <>
      <h1 className="display-1">Store</h1>
      <div className="mb-2">{renderFilters()}</div>
      <div className="mb-2">{renderPagination()}</div>
      <div className="d-flex flex-column">
        {storeItems.map(item =>
          <div key={item.id} className="mb-2">
            <StoreItem {...item} />
          </div>
        )}
      </div>
      {renderPagination()}
    </>
  )

  function renderFilters() {
    const handleCategoryChange = (event) => {
      setCurrentCategoryId(event.target.value);
      setCurrentPage(1);
    }
    const handleSortByChange = (event) => {
      setSortBy(event.target.value);
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
    const handleAscendingDirection = () => setSortDirection(0);
    const handleDescendingDirection = () => setSortDirection(1);

    return (
      <div className="d-flex">
        <InputGroup>
          <InputGroup.Text>Category</InputGroup.Text>
          <FormSelect value={currentCategoryId} onChange={handleCategoryChange}>
            <option value="">All</option>
            {categories.map(item => 
              <option key={item.id} value={item.id}>
                {item.name}
              </option>
            )}
          </FormSelect>
        </InputGroup>

        <InputGroup className="ms-2">
          <InputGroup.Text>Sort by</InputGroup.Text>
          <FormSelect value={sortBy} onChange={handleSortByChange}>
            <option value="Name">Name</option>
            <option value="Price">Price</option>
            <option value="Brand">Brand</option>
            <option value="Category">Category</option>
          </FormSelect>
          <Button size="sm" onClick={handleAscendingDirection}><ArrowUp/></Button>
          <Button size="sm" onClick={handleDescendingDirection}><ArrowDown/></Button>
        </InputGroup>

        <form onSubmit={handleSearchClick} className="w-100 ms-2">
          <InputGroup>
            <FormControl placeholder="Search" value={searchPhraseTmp} onChange={handleSearchChange}/>
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
    for (let i = 1; i <= storePage.totalPages; i++) {
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
          <InputGroup.Text>Products per page</InputGroup.Text>
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
