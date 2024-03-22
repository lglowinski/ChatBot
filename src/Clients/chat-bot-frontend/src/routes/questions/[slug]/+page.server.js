import { error } from '@sveltejs/kit';

export async function load({ params }) {
    const slug = params.slug;
    console.log(slug)
    const res = await fetch(`${import.meta.env.VITE_API_URL}/api/questions/${slug}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json, text/plain'
        }
    })

    const question = await res.json();

    console.log(question)

    if (!question) throw error(404);

    return {
        question
    };
}
