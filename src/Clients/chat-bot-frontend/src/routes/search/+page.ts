import {type Load} from '@sveltejs/kit';
import { search } from "../../lib/api";

export const load: Load = async({url}) => {
    const searchTerm = url.searchParams.get('searchTerm');

    const response = await search(`searchTerm=${searchTerm}&orderBy=HelpfulCount&page=0&take=10`);

    const questions : QuestionListElement[] = response.questions.map((q) => ({
        id: q.id,
        summary: q.summary,
        title: q.title,
        createdAt: new Date(q.createdAt)}));

    return {
        searchTerm,
        questions
    };
}