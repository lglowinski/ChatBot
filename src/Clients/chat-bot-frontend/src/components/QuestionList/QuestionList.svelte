<script lang="ts">
    import {onMount} from 'svelte';

    type Question = {
        id: string;
        title: string;
        summary: string;
        createdAt: Date;
    };

    let questions: Question[] = [];

    async function fetchQuestions() {
        const response = await fetch(`${import.meta.env.VITE_API_URL}/api/questions?orderBy=CreatedAt&take=10`);
        if (response.ok) {
            const data = await response.json();
            questions = data.questions.map((q) => ({
                ...q,
                createdAt: new Date(q.createdAt) // Konwertuj string na obiekt Date
            }));
        } else {
            console.error('Failed to fetch questions:', response.statusText);
        }
    }

    onMount(async () => {
        await fetchQuestions();
    });
</script>

<main>
    <slot/>
</main>

<aside>
    <section>
        <div class="max-w-5xl mx-auto">
            <h2 class="text-2xl font-semibold mb-5">Recent questions</h2>
            <div class="space-y-0">
                {#each questions as question}
                    <div class="bg-[#121C24] rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow group">
                        <a href="questions/{question.id}"
                           class="group-focus:border-blue-500 group-focus:ring-blue-500 focus:underline">
                            <div class="p-4">
                                <span class="font-semibold">{question.title}</span> <span
                                    class="text-gray-400">- {question.createdAt.toLocaleDateString()}</span>
                                <p class="text-gray-600 text-sm">{question.summary}</p>
                            </div>
                        </a>
                    </div>
                {/each}
            </div>
        </div>
    </section>
</aside>