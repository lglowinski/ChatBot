<script lang="ts">
    export let title : string;
    export let questions: QuestionListElement[] = [];
</script>

<main>
    <slot/>
</main>

<aside>
    <section>
        <div class="max-w-5xl mx-auto">
            <h2 class="text-2xl font-semibold mb-5">{title}</h2>
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