import { error } from '@sveltejs/kit';
import type { Load } from '@sveltejs/kit';
import { details } from '../../../lib/api';

export const load: Load = async({params, fetch}) => {
    const id = params.id;

    if(!id) throw error(404);
    
    const question = await details(id, fetch);

    console.log(question)

    if (!question) throw error(404);

    return {
        question
    };
}
