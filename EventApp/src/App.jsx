import { useState, useEffect } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import axios from 'axios'
import { Container } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'

function App() {
    const [events, setEvents] = useState([])
    const [paginationModel, setPaginationModel] = useState({
        pageSize: 5,
        page: 0,
    })

    const columns = [
        { field: 'name', headerName: 'Event Name', width: 200 },
        { field: 'location', headerName: 'Location', width: 200 },
        {
            field: 'startsOn',
            headerName: 'Start Date',
            width: 250,
            valueFormatter: (params) =>
                new Date(params).toLocaleString()
        },
        {
            field: 'endsOn',
            headerName: 'End Date',
            width: 250,
            valueFormatter: (params) =>
                new Date(params).toLocaleString()
        }
    ]

    const axiosInstance = axios.create({
        baseURL: 'https://localhost:7208/Events',
    })

    async function fetchEvents() {
        try {
            const response = await axiosInstance.get('/GetEvents')
            setEvents(response.data)
        } catch (error) {
            console.error('Error fetching events:', error)
        }
    }

    useEffect(() => {
        fetchEvents()
    }, [])

    return (
        <Container maxWidth="lg">
            <div style={{ marginBottom: '2rem' }}>
                <a href="https://vite.dev" target="_blank">
                    <img src={viteLogo} className="logo" alt="Vite logo" />
                </a>
                <a href="https://react.dev" target="_blank">
                    <img src={reactLogo} className="logo react" alt="React logo" />
                </a>
            </div>
            <div style={{ height: 400, width: '100%' }}>
                <DataGrid
                    rows={events}
                    columns={columns}
                    paginationModel={paginationModel}
                    onPaginationModelChange={setPaginationModel}
                    pageSizeOptions={[5, 10, 25]}
                    disableRowSelectionOnClick
                    autoHeight
                />
            </div>
        </Container>
    )
}

export default App