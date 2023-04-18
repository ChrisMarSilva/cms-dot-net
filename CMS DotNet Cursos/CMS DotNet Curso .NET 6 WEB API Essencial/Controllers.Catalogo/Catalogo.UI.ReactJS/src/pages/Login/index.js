import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom'; // useHistory
import './styles.css';
import api from '../../services/api';
import logoImage from '../../assets/login.png';

export default function Login() {

    const [email, setEmail] = useState('user3@example.com');
    const [password, setPassword] = useState('stringstri');
    const [authenticated, setauthenticated] = useState(
        localStorage.getItem(localStorage.getItem("authenticated") || false)
    );
    // const history = useHistory();
    const navigate = useNavigate();

    async function handleSubmit(event) {
        event.preventDefault();
        try {

            const data = { email, password };
            const url = 'api/v1/autoriza/login'; // 'api/account/loginuser'
            const response = await api.post(url, data);

            localStorage.setItem("authenticated", true);
            localStorage.setItem('email', email);
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('expiration', response.data.expiration);

            // history.push('/alunos');
            navigate('/alunos');
        } catch (error) {
            alert('O login falhou ' + error)
        }
    }

    return (
        <div className="login-container">
            <section className="form">
                <img src={logoImage} alt="Login" id="img1" />
                <form onSubmit={handleSubmit}>
                    <h1>Cadastro de Alunos</h1>
                    <input placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} />
                    <input type="password" placeholder="Password" value={password} onChange={e => setPassword(e.target.value)} />
                    <button class="button" type="submit">Login</button>
                </form>
            </section>
        </div>
    )
}