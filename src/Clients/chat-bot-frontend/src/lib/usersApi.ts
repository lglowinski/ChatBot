export type RegisterUserRequest = {
    login: string,
    password: string
};

export type RegisterUserResponse = {
    id: string,
    email: string,
    secretKey: string,
    qrCodeUri: string
};


export async function register(login: string, password:string): Promise<RegisterUserResponse> {
    const request: RegisterUserRequest = {
        login,
        password
    };
    const res = await fetch(`/user-api/users/register`, {
        method: 'POST',
        body: JSON.stringify(request),
        headers: {
            'Accept': 'application/json, text/plain',
            'Content-Type': 'application/json;charset=UTF-8'
        },
    })

    const response: RegisterUserResponse = await res.json();

    return response;
}