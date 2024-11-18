export type QuestionListResponse = {
    questions:[
    id: string,
    title: string,
    summary: string,
    createdAt: Date,
    ]
};

type QuestionDetails = {
    title: string,
    answer: string,
    upvotes: string,
    downvotes: string
}

interface AskQuestionRequest {
    question: string
}

interface AskQuestionResponse {
    id: string,
    title: string,
    answer: string,
    upvotes: number,
    downvotes: number
}

interface LikeQuestionRequest{
    liked: boolean,
    disliked: boolean
}

export async function search(query: string): Promise<QuestionListResponse> {
    const response = await fetch(`/api/questions?${query}`);
    if (!response.ok) {
        throw new Error('Network response was not ok');
    }
    return response.json();
}

export async function details(id: string, eventFetch: {
    (input: (string | URL | globalThis.Request), init?: RequestInit): Promise<Response>;
    (input: (RequestInfo | URL), init?: RequestInit): Promise<Response>;
    (input: (RequestInfo | URL), init?: RequestInit): Promise<Response>
}): Promise<QuestionDetails> {

    const response = await eventFetch(`/api/questions/${id}`);
    if (!response.ok) {
        console.log(response);
        throw new Error('Network response was not ok');
    }
    return response.json();
}

export async function ask(question: string): Promise<string> {
    const request: AskQuestionRequest = {
        question: question
    };

    const res = await fetch(`/api/questions`, {
        method: 'POST',
        body: JSON.stringify(request),
        headers: {
            'Accept': 'application/json, text/plain',
            'Content-Type': 'application/json;charset=UTF-8'
        },
    })

    const response: AskQuestionResponse = await res.json();

    return response.id;
}

export async function like(id: string, liked: boolean, disliked:boolean) : Promise<void> {
    const request: LikeQuestionRequest = {
        liked: liked,
        disliked: disliked
    };

    const response = await fetch(`/api/questions/${id}/likes`, {
        method: 'PUT',
        body: JSON.stringify(request),
        headers: {
            'Accept': '*/*',
            'Content-Type': 'application/json'
        },
    })

    if (!response.ok) {
        console.log(await response.text())
        throw new Error('Failed to like question');
    }
}

export async function markHelpful(id: string) : Promise<void> {
    const response = await fetch(`/api/questions/${id}/helpful`, {
        method: 'PUT',
        headers: {
            'Accept': '*/*',
            'Content-Type': 'application/json'
        },
    })

    if (!response.ok) {
        throw new Error('Failed to like question');
    }
}