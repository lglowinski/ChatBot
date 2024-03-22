<script lang="ts">
    import { goto } from "$app/navigation";

    interface AskQuestionRequest{
        question: string
    }

    interface AskQuestionResponse{
        id: string,
        title: string,
        answer: string,
        upvotes: number,
        downvotes: number
    }

    let question = "";

    const submitQuestion = async (): Promise<void> => {
        if(question.trim().length > 0){
            console.log(`Question asked: ${question}`)
            const request: AskQuestionRequest = {
                question: question
            };

            console.log(import.meta.env.VITE_API_URL)

            const res = await fetch(`${import.meta.env.VITE_API_URL}/api/questions`, {
                method: 'POST',
                body: JSON.stringify(request),
                headers: {
                    'Accept': 'application/json, text/plain',
                    'Content-Type': 'application/json;charset=UTF-8'
                },
            })

            const response: AskQuestionResponse = await res.json();

            console.log(response);

            navigateToQuestion(response.id);

            question = "";
        }
    }

    const navigateToQuestion = (id: string) => {
        goto(`/questions/${id}`)
    }
</script>

<div class="w-full flex justify-center items-center pt-5">
    <div class="relative text-white mx-auto text-center overflow-hidden max-h-[480px] max-w-[928px] w-2/3 opacity-90">
        <img src="banner.webp" alt="Banner Background" class="w-full h-full object-cover absolute top-0 left-0 z-0" />
        <div class="relative z-10 p-12 md:p-24 justify-center items-center">
            <h1 class="text-4xl font-bold mb-4 font-48">Welcome to AvatarUI</h1>
            <p class="text-xl mb-8">Ask anything. Get answers.</p>
            <div class="relative shadow md:block">
                <div class="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none ">
                    <svg class="w-4 h-4 text-gray-500 dark:text-gray-400"
                         aria-hidden="true"
                         xmlns="http://www.w3.org/2000/svg" fill="none"
                         viewBox="0 0 20 20">
                        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                              d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"/>
                    </svg>
                    <span class="sr-only">Search icon</span>
                </div>
                <input type="text" id="ask-question"
                       class="block w-full p-2 ps-10 min-h-12 text-sm text-[#8A9EBF] border border-gray-300 rounded-lg bg-gray-50
					    focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-900 dark:border-gray-600
					    dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500
                        md:flex"
                       placeholder="What do you want to know?"
                       bind:value={question} on:keyup={event => {if (event.key === 'Enter') submitQuestion()}}
                >
                <button class="absolute inset-y-0 right-0 mr-1 mt-1 mb-1 flex items-center bg-[#F5C754] text-gray-900 text-sm font-bold px-4
                rounded shadow-md transition duration-300 ease-in-out transform hover:scale-10"
                on:click={submitQuestion}>
                    Submit
                </button>
            </div>
        </div>
    </div>
</div>
