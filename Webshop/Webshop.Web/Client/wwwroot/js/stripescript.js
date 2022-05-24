
let stripe, customer, price, card;
stripe = window.Stripe('pk_test_51HsZbzBkXnJ98OJr1J1FhYXipd6r7zkVfiCSxqyAKyvtVQukCfYD8FIDt5cRqJ5HT7aSclkwIOLabGhY7OXmqbfX00KesyaFAy');
let elements = stripe.elements();
let  style = {
    base: {
        color: '#32325d',
        fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
        fontSmoothing: 'antialiased',
        fontSize: '16px',
        '::placeholder': {
            color: '#aab7c4'
        }
    },
    invalid: {
        color: '#fa755a',
        iconColor: '#fa755a'
    }
};
let startcard = true;
function Initiate() {
    if (startcard) {
        card = elements.create('card', { style: style });
        startcard = false;
    }
    card.mount('#card-element');
    card.on('change', function (event) {
        displayError(event);
    });
}

function displayError(event) {

    let displayError = document.getElementById('card-element-errors');

    if (event.error) {
        displayError.textContent = event.error.message;
    } else {
        displayError.textContent = '';
    }
}

function createPaymentMethod(dotnetHelper)
{
    return stripe
        .createPaymentMethod({
            type: 'card',
            card: card,
            billing_details: {
                name: 'asdasd',
                email: 'asd@asdasd.com',
            },
        })
        .then((result) => {
            if (result.error) {
                console.log('POGGERS1');
                displayError(result);
            } else {
                console.log('POGGERS2');
                createSubscription(dotnetHelper, result.paymentMethod.id );
            }
        }, (result) => { console.log('POGGERS3'); });
}

function createPaymentMethodServer(dotnetHelper)
{
    createPaymentMethod(dotnetHelper);
}

function createSubscription(dotnetHelper, paymentMethodId)
{
    dotnetHelper.invokeMethodAsync('Subscribe', paymentMethodId);
    dotnetHelper.dispose();
}