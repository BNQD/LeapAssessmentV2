import { useState, useEffect } from 'react'
import { Container } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import axios from 'axios'

function EventList() {
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
           alert('Error fetching events:', error)
        }
    }

    useEffect(() => {
        fetchEvents()
    }, [])

    return (
        <div style={{ height: 400, width: '100%' }}>
           <h1> Events </h1>
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
    )
}

export default EventList