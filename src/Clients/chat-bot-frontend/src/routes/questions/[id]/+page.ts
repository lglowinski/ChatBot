import type { PageLoad } from './$types';

export const load: PageLoad = async ({ params }) => {
    if (!params.id) {
        throw new Error('No ID provided');
    }

    const question = {
        title: "Hi from question",
        id: params.id
    };

    return { props: { question } };
};
