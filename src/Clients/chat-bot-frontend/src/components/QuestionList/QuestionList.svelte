<script lang="ts">
    import {onMount} from 'svelte';
    import { goto } from '$app/navigation';

    type Question = {
        id: string;
        title: string;
        question: string;
        created: string;
    };

    let questions: Question[] = [];

    async function fetchQuestions() {
        // const response = await fetch('api.address/api/questions?orderBy=created&take=10'); TO
        // if (response.ok) {
        //     const data = await response.json();
        //     questions = data;
        // } else {
        //     console.error('Failed to fetch questions:', response.statusText);
        // }
        questions = [
            {
                id: "1",
                title: "How to cock eggs?",
                question: "How long should I cook eggs?",
                created: "2024-03-04"
            },
            {
                id: "2",
                title: "How far is moon?",
                question: "How long should I cook eggs?",
                created: "2024-03-04"
            },
            {
                id: "3",
                title: "How to find gold?",
                question: "How long should I cook eggs?",
                created: "2024-03-04"
            },
            {
                id: "4",
                title: "Who was in paris?",
                question: "How long should I cook eggs?",
                created: "2024-03-04"
            },
            {
                id: "5",
                title: "How many seconds is in a day?",
                question: "How long should I cook eggs?",
                created: "2024-03-04"
            }
        ]
    }

    onMount(async () => {
        await fetchQuestions();
    });
</script>

<section>
    <div class="max-w-5xl mx-auto">
        <h2 class="text-2xl font-semibold mb-5">Recent questions</h2>
        <div class="space-y-0">
            {#each questions as question}
                <div class="bg-[#121C24] rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow group">
                <a href="questions/{question.id}" class="group-focus:border-blue-500 group-focus:ring-blue-500 focus:underline">
                    <div class="p-4">
                        <span class="font-semibold">{question.title}</span> <span class="text-gray-400">- {question.created}</span>
                        <p class="text-gray-600 text-sm">{question.question}</p>
                    </div>
                </a>
                </div>
            {/each}
        </div>
    </div>
</section>