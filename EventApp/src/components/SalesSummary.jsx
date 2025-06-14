import { useState, useEffect } from 'react'
import { Container, Button, Box, Typography } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import axios from 'axios'

function SalesSummary() {
    const [events, setEvents] = useState([])
    const [viewType, setViewType] = useState('sales') 

    const columns = [
        { field: 'name', headerName: 'Event Name', width: 300 },
        {
            field: 'totalSales',
            headerName: 'Total Sales',
            width: 200,
            valueFormatter: (params) => "$" + (params / 100).toFixed(2)
        },
        {
            field: 'ticketCount',
            headerName: 'Tickets Sold',
            width: 200
        }
    ];

    const axiosInstance = axios.create({
        baseURL: 'https://localhost:7208/Events',
    })

    async function fetchEvents() {
        try {
            const endpoint = viewType === 'sales'
                ? '/GetTopFiveHighestSellingEvents'
                : '/GetTopFiveHighestCountEvents'
            const response = await axiosInstance.get(endpoint);
            //throw new Error('test');
            setEvents(response.data)
        } catch (error) {
            alert('Error fetching events:', error)
        }
    }

    useEffect(() => {
        fetchEvents()
    }, [viewType])

    const toggleView = () => {
        setViewType(prevType => prevType === 'sales' ? 'count' : 'sales')
    }

    return (
        <Container maxWidth="lg">
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
                <Typography variant="h4" component="h1">
                    Sales Summary
                </Typography>
                <Button
                    variant="contained"
                    onClick={toggleView}
                >
                    Show Top 5 by {viewType === 'sales' ? 'Count' : 'Sales'}
                </Button>
            </Box>
            <Typography variant="h6" sx={{ mb: 2 }}>
                {viewType === 'sales' ? 'Top 5 Events by Sales' : 'Top 5 Events by Ticket Count'}
            </Typography>
            <div style={{ height: 400, width: '100%' }}>
                <DataGrid
                    rows={events}
                    columns={columns}
                    disableRowSelectionOnClick
                    autoHeight
                />
            </div>
        </Container>
    )
}

export default SalesSummary