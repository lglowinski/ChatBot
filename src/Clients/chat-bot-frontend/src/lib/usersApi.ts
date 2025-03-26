export type RegisterRequest = {
    email: string;
    password: string;
};

export type LoginRequest = RegisterRequest;

export type VerifyRequest = {
    email: string;
    twoFactorCode: string;
};

export type RegisterResponse = {
    userId: string;
    email: string | null;
    secretKey: string | null;
    qrCodeUri: string | null;
};

export type VerifyResponse = {
    token: string | null;
    userEmail: string | null;
};

const API_BASE_URL = '/users-api/users'; // Zmiana base URL

export async function register(email: string, password: string): Promise<RegisterResponse> {
    const request: RegisterRequest = {
        email,
        password
    };

    const response = await fetch(`${API_BASE_URL}/register`, {
        method: 'POST',
        body: JSON.stringify(request),
        headers: {
            'Content-Type': 'application/json'
        },
    });

    if (!response.ok) {
        throw new Error(await response.text());
    }

    return await response.json();
}

export async function login(email: string, password: string): Promise<RegisterResponse> {
    const request: LoginRequest = {
        email,
        password
    };

    const response = await fetch(`${API_BASE_URL}/login`, {
        method: 'POST',
        body: JSON.stringify(request),
        headers: {
            'Content-Type': 'application/json'
        },
    });

    if (!response.ok) {
        throw new Error(await response.text());
    }

    return await response.json();
}

export async function verify(email: string, twoFactorCode: string): Promise<VerifyResponse> {
    const request: VerifyRequest = {
        email,
        twoFactorCode
    };

    const response = await fetch(`${API_BASE_URL}/verify`, {
        method: 'POST',
        body: JSON.stringify(request),
        headers: {
            'Content-Type': 'application/json'
        },
    });

    if (!response.ok) {
        throw new Error(await response.text());
    }

    return await response.json();
}