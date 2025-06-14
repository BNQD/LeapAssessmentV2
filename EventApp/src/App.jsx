
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom'
import './App.css'
import { Container, AppBar, Toolbar, Button, Typography, Box } from '@mui/material'
import EventList from './components/EventList'
import SalesSummary from './components/SalesSummary'

function App() {
    return (
        <BrowserRouter>
            <Container maxWidth="lg">
                <AppBar position="static" sx={{ marginBottom: 3 }}>
                    <Toolbar>
                        <Button color="inherit" component={Link} to="/">
                            Events
                        </Button>
                        <Button color="inherit" component={Link} to="/sales">
                            Sales Summary
                        </Button>
                    </Toolbar>
                </AppBar>

                <Routes>
                    <Route path="/" element={<EventList />} />
                    <Route path="/sales" element={<SalesSummary />} />
                </Routes>
            </Container>
        </BrowserRouter>
    )
}

export default App